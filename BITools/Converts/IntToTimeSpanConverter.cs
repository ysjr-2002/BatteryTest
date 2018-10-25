using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BITools.Converts
{
    /// <summary>
    /// 5 to 00:00:05
    /// </summary>
    public class IntToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToInt32(value);
            var ts = TimeSpan.FromSeconds(val);
            var str = string.Format("{0}:{1}:{2}", ((int)ts.TotalHours).ToString("d2"), ((int)ts.TotalMinutes).ToString("d2"), ((int)ts.TotalSeconds).ToString("d2"));
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
