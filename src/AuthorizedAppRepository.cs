using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logging
{
    /// <summary>
    /// 模拟db 授权的 appid appsecret
    /// </summary>
    public class AuthorizedAppRepository
    {
        public static IEnumerable<string> GetAppIds()
        {
            return new string[] { "xf2dsdfnlinR" };
        }
    }
}
