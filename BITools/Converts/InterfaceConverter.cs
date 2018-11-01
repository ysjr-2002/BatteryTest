using BITools.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BITools.Converts
{
    class InterfaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "未知";

            var val = System.Convert.ToInt32(value);
            if (val == (int)InterfaceEnum.AAA)
                return "485";
            else if (val == (int)InterfaceEnum.Com)
                return "串口";
            else
                return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
