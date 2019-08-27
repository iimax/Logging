using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.Models
{
    /// <summary>
    /// log4net log model
    /// </summary>
    public class DeviceLogModel
    {
        public string DeviceNO { get; set; }

        public DateTime TimeStamp { get; set; }

        public string UserName { get; set; }

        public string ThreadName { get; set; }

        public string RenderedMessage { get; set; }

        /// <summary>
        /// will mark as obsolete
        /// </summary>
        public Exception ExceptionObject { get; set; }

        /// <summary>
        /// exception object as string
        /// </summary>
        public string Exceptions { get; set; }
        
        public string LoggerName { get; set; }

        public string Domain { get; set; }

        public string Identity { get; set; }
        
        public string Level { get; set; }
    }
}
