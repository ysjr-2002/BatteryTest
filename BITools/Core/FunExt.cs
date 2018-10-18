using System;
using System.Collections.Generic;
using System.IO.Ports;
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

        public static int ToInt32(this string str)
        {
            var i = 0;
            Int32.TryParse(str, out i);
            return i;
        }

        public static List<string> GetSerialPorts()
        {
            var items = SerialPort.GetPortNames();
            return items.ToList();
        }
    }
}
