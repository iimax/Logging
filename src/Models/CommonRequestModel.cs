using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.Models
{
    /// <summary>
    /// 通用的请求模型
    /// </summary>
    public class CommonRequestModel
    {
        public string appid { get; set; }

        public string payload { get; set; }

        public string timestamp { get; set; }

        public string sign { get; set; }
    }
}
