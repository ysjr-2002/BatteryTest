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

        public static DateTime ToDateTime(this string str)
        {
            return Convert.ToDateTime(str);
        }

        public static string ToStandard(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static List<string> GetSerialPorts()
        {
            var items = SerialPort.GetPortNames().ToList();
            items.Clear();
            items.Insert(0, "无");
            items.Insert(1, "COM1");
            items.Insert(2, "COM2");
            items.Insert(3, "COM3");
            items.Insert(4, "COM4");
            items.Insert(5, "COM5");
            items.Insert(6, "COM6");
            return items;
        }
    }
}
