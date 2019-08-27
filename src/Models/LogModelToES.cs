using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.Models
{
    /// <summary>
    /// log structure will written into elasticsearch
    /// </summary>
    public class LogModelToES
    {
        public string DeviceId { get; set; }

        public DateTime Time { get; set; }

        /// <summary>
        /// device time
        /// </summary>
        public string LocalTime { get; set; }

        public string Product { get; set; }

        public string Msg { get; set; }

        public string Level { get; set; }

        public Exception Exception { get; set; }

        /// <summary>
        /// exception object as string
        /// </summary>
        public string Exceptions { get; set; }
    }
}
