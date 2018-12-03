using BICommon.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BICommon
{
    public static class FunExt
    {
        public static string getUUID()
        {
            return Guid.NewGuid().ToString("N");
        }

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

        public static int ToInt32(this object obj)
        {
            if (obj == null)
                return 0;

            var i = 0;
            Int32.TryParse(obj.ToString(), out i);
            return i;
        }

        public static float ToFloat(this string str)
        {
            var i = 0f;
            float.TryParse(str, out i);
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
            //items.Insert(0, "无");
            items.Add("COM1");
            items.Add("COM2");
            items.Add("COM3");
            items.Add("COM4");
            items.Add("COM5");
            items.Add("COM6");
            items.Add("COM7");
            items.Add("COM8");
            items.Add("COM9");
            items.Add("COM10");
            return items;
        }

        public static List<string> Bauds()
        {
            var items = SerialPort.GetPortNames().ToList();
            items.Clear();
            items.Add("4800");
            items.Add("7200");
            items.Add("9600");
            items.Add("14400");
            items.Add("19200");
            items.Add("38400");
            items.Add("57600");
            items.Add("115200");
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

        /// <summary>
        /// 测试上下限
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCSSXX()
        {
            var items = new List<string>();
            items.Insert(0, "测试上下限1");
            items.Insert(1, "测试上下限2");
            return items;
        }

        public static string GetRepeatUnitName(int index)
        {
            var unit = (RepeatUnitEnum)index;
            if (unit == RepeatUnitEnum.Fen)
                return "分";
            else
                return "次";
        }

        public static string GetTimeUnitName(int index)
        {
            var unit = (TimeUnitEnum)index;
            if (unit == TimeUnitEnum.Hour)
                return "时";
            else if (unit == TimeUnitEnum.Minute)
            {
                return "分";
            }
            else
                return "秒";
        }

        public static string JsonFormatter(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
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

        public static string IntToTimeSpan(int val)
        {
            var ts = TimeSpan.FromSeconds(val);
            var str = string.Format("{0}:{1}:{2}", ((int)ts.Hours).ToString("d2"), ((int)ts.Minutes).ToString("d2"), ((int)ts.Seconds).ToString("d2"));
            return str;
        }
    }
}
