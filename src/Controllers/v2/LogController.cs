using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Logging.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;

namespace Logging.Controllers.v2
{
    // 计划此版本增加签名校验
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly IElasticClient _elasticClient;
        private readonly IMapper _mapper;
        private readonly ILogger<LogController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public LogController(IElasticClient elasticClient, IMapper mapper, ILogger<LogController> logger, IHttpClientFactory clientFactory)
        {
            _elasticClient = elasticClient;
            _mapper = mapper;
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpPost]
        public void Post([FromBody]CommonRequestModel model)
        {
            try
            {
                // validate sign

                // same appid&timestamp must occur once only


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to log");
            }
        }
    }
}