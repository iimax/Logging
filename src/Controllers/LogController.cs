using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Logging.Controllers
{
    // 日志上报统一入口
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LogController : ControllerBase
    {
        private IElasticClient _elasticClient;
        public LogController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody]DeviceLogModel model)
        {
            //var settings = new ConnectionSettings(new Uri("http://127.0.0.1:9200")).DefaultIndex("logs");
            //_elasticClient = new ElasticClient(settings);

            try
            {
                if (DeviceNOBasedIndexCreator.NotExists(model.DeviceNO))
                {
                    var x = await _elasticClient.Indices.ExistsAsync($"logs-{model.DeviceNO}");
                    if (!x.Exists)
                    {
                        var createIndexResponse = await _elasticClient.Indices.CreateAsync($"logs-{model.DeviceNO}");
                        if (createIndexResponse.Acknowledged)
                        {
                            DeviceNOBasedIndexCreator.Created(model.DeviceNO);
                        }
                    }
                    else
                    {
                        DeviceNOBasedIndexCreator.Created(model.DeviceNO);
                    }
                }
                
                
                var log = new LogModelToES { Time = DateTime.Now, Level = model.Level, Msg = model.RenderedMessage };
                //_elasticClient.Index
                var l = await _elasticClient.IndexAsync(model, (a)=> { return a.Index($"logs-{model.DeviceNO}"); } );
                var response = await _elasticClient.IndexDocumentAsync(model);
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

                throw;
            }
            
        }
    }
}