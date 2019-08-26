using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.Models
{
    /// <summary>
    /// 定义写入ES里的日志结构
    /// </summary>
    public class LogModelToES
    {
        public DateTime Time { get; set; }

        public string Product { get; set; }

        public string Msg { get; set; }

        public string Level { get; set; }

        public Exception Exception { get; set; }
    }
}
