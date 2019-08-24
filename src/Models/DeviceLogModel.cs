using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging.Models
{
    public class DeviceLogModel
    {
        public DateTime Time { get; set; }

        public string Level { get; set; }

        public string Msg { get; set; }
    }
}
