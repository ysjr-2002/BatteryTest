using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools
{
    static class FunExt
    {
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
