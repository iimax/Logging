using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.Models
{
    public class DeviceLogModel
    {
        public string DeviceNO { get; set; }

        public DateTime TimeStamp { get; set; }

        public string UserName { get; set; }

        public string ThreadName { get; set; }

        public string RenderedMessage { get; set; }

        /// <summary>
        /// 逐渐废弃此属性
        /// </summary>
        public Exception ExceptionObject { get; set; }

        /// <summary>
        /// 将exception用字符串传递
        /// </summary>
        public string Exceptions { get; set; }
        
        public string LoggerName { get; set; }

        public string Domain { get; set; }

        public string Identity { get; set; }
        
        public string Level { get; set; }
    }
}
