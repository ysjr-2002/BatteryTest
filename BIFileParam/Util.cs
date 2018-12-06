using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIFileParam
{
    public static class Util
    {
        public static string GetActiveValToBoolDisplay(string val)
        {
            if (val == "0")
                return "True";
            else
                return "False";
        }

        public static string GetActiveBoolToValDisplay(string val)
        {
            if (val == "True")
                return "0";
            else
                return "-1";
        }
    }
}
