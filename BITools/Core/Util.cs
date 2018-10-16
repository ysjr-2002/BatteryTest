using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.Core
{
    static class Util
    {
        public static string getUUID()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static string ToStandard(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
