using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
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

        public static List<string> GetSaveRoot()
        {
            var items = new List<string>();
            items.Insert(0, "");
            items.Insert(1, "日期");
            items.Insert(2, "机型名");
            items.Insert(3, "区域");
            return items;
        }

        public static List<Dictonary> GetQuJian()
        {
            var items = new List<Dictonary>();
            items.Insert(0, new Dictonary { Name = "全部", Value = "0" });
            return items;
        }

        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();   //获取类型
            MemberInfo[] memberInfos = type.GetMember(en.ToString());   //获取成员
            if (memberInfos != null && memberInfos.Length > 0)
            {
                DescriptionAttribute[] attrs = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];   //获取描述特性
                if (attrs != null && attrs.Length > 0)
                {
                    return attrs[0].Description;    //返回当前描述
                }
            }
            return en.ToString();
        }
    }
}
