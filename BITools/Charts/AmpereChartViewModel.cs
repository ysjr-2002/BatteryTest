using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BITools.Charts
{
    public class AmpereChartViewModel
    {
        public ChartValues<double> SeriesValues { get; set; }

        public Func<double, string> XFormatter { get; set; }

        public Func<double, string> YFormatter { get; set; }

        public AmpereChartViewModel()
        {
            SeriesValues = new ChartValues<double>();
            for (int i = 0; i < 500; i++)
            {
                Random r = new Random(i);
                double value = 7 + r.Next(0, 100) * 0.001;
                SeriesValues.Add(value);
            }

            XFormatter = ConvertXValue;
            YFormatter = ConvertYValue;
        }

        private string ConvertXValue(double value)
        {
            return value.ToString("F1");
        }

        private string ConvertYValue(double value)
        {
            if (value == 0) return value.ToString("F1");
            return value.ToString("F2");
        }
    }
}
