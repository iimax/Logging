using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging
{
    public class DeviceNOBasedIndexCreator
    {
        public static ConcurrentDictionary<string, bool> Indices = new ConcurrentDictionary<string, bool>();

        public static bool NotExists(string deviceNO)
        {
            return !Indices.ContainsKey(deviceNO);
        }

        public static void Created(string deviceNO)
        {
            Indices.TryAdd(deviceNO, true);
        }
    }
}
