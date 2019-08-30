using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Logging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using Logging.Filters;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Logging.Controllers
{
    // 日志上报统一入口
    [TokenValidationFilter]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;
        private readonly ILogger<LogController> _logger;
        private readonly bool _logByDevice = true;
        private readonly IHttpClientFactory _clientFactory;

        public LogController(IElasticClient elasticClient, IMapper mapper, ILogger<LogController> logger, IHttpClientFactory clientFactory)
        {
            _elasticClient = elasticClient;
            _mapper = mapper;
            _logger = logger;
            _clientFactory = clientFactory;
        }

        // POST api/v{}/Log 将设备端的时间戳转换为字符串，以服务器的时间来作为es的filter
        [HttpPost]
        public async void Post([FromBody]DeviceLogModel model)
        {
            try
            {
                if (model == null)
                {
                    return;
                }
                string deviceId = string.Empty, product = string.Empty;
                if (string.IsNullOrWhiteSpace(model.DeviceNO))
                {
                    _logger.LogError("unknown log received: {model}", model);
                    return;
                }
                //
                var idx = model.DeviceNO.IndexOf("-");
                if (idx < 0)
                {
                    _logger.LogError("unknown device log received: {model}", model);
                    return;
                }
                deviceId = model.DeviceNO.Substring(0, idx);
                product = model.DeviceNO.Substring(idx + 1);
                if (string.IsNullOrWhiteSpace(deviceId))
                {
                    deviceId = "00000";
                }
                if (_logByDevice)
                {
                    if (DeviceNOBasedIndexCreator.NotExists(deviceId))
                    {
                        var x = await _elasticClient.Indices.ExistsAsync($"logs-{deviceId}");
                        _logger.LogDebug("logs-{deviceId} exist={x}", deviceId, x);
                        if (!x.Exists)
                        {
                            var createIndexResponse = await _elasticClient.Indices.CreateAsync($"logs-{deviceId}");
                            _logger.LogDebug("create index logs-{deviceId} result={createIndexResponse}", deviceId, createIndexResponse);
                            if (createIndexResponse.Acknowledged)
                            {
                                DeviceNOBasedIndexCreator.Created(deviceId);
                            }
                        }
                        else
                        {
                            DeviceNOBasedIndexCreator.Created(deviceId);
                        }
                    }
                }
                

                var log = _mapper.Map<LogModelToES>(model);
                log.Product = product;
                log.Time = DateTime.Now.ToUniversalTime();
                log.DeviceId = deviceId;
                // ThreadId, Logger

                if (_logByDevice)
                {
                    var l = await _elasticClient.IndexAsync(log, (a) => { return a.Index($"logs-{deviceId}"); });
                    _logger.LogDebug("index doc on logs-{deviceId} result={l}", deviceId, l);
                }
                else
                {
                    var response = await _elasticClient.IndexDocumentAsync(log);
                    _logger.LogDebug("index doc on logs result={response}", response);
                }


                try
                {
                    var client = _clientFactory.CreateClient("bbysDEVOPS");
                   

                    model.DeviceNO = deviceId;
                    model.RenderedMessage = string.Format("{0} {1}", product, model.RenderedMessage);
                    var str = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var content = new StringContent(str, Encoding.UTF8, "application/json");
                    var msg = await client.PostAsync("index/Api/upload_log", content);
                    //logger.InfoFormat("upload_log post {0}, ok={1}", str, msg.IsSuccessStatusCode);
                    //msg.EnsureSuccessStatusCode();
                    //var rs = msg.Content.ReadAsStringAsync().Result;
                    //logger.InfoFormat("upload_log response={0}", rs);
                }
                catch (Exception exx)
                {
                    _logger.LogError(exx, "上报yw日志出错");
                }
                //返回结构示例：
                /*# POST /db/user/1
    {
      "_index": "db",
      "_type": "user",
      "_id": "1",
      "_version": 1,
      "result": "created",
      "_shards": {
        "total": 2,
        "successful": 1,
        "failed": 0
      },
      "_seq_no": 2,
      "_primary_term": 1
    }*/
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to log");
            }
            
        }
    }
}