using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logging.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace Logging.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IElasticClient _elasticClient;
        public ValuesController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogModelToES>>> Get()
        {
            //var searchResponse = await _elasticClient.SearchAsync<LogModelToES>(s => s
            //    .From(0)
            //    .Size(10)
            //    .Query(q => q
            //         .Match(m => m
            //            .Field(f => f.Msg)
            //            .Query("price")
            //         )
            //    )
            //);
            var searchResponse = await _elasticClient.SearchAsync<LogModelToES>();
            var people = searchResponse.Documents;
            return people.ToArray();
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async void Post([FromBody] string value)
        {
            //var settings = new ConnectionSettings(new Uri("http://127.0.0.1:9200")).DefaultIndex("logs");

            //_elasticClient = new ElasticClient(settings);

            var log = new DeviceLogModel { TimeStamp = DateTime.Now, Level = "INFO", RenderedMessage = value };
            //var indexResponse = _elasticClient.IndexDocument(log);
            var response = await _elasticClient.IndexDocumentAsync(log);
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
