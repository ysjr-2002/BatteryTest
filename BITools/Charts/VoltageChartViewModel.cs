using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BITools.Charts
{
    public class VoltageChartViewModel
    {
        public ChartValues<double> SeriesValues { get; set; }

        public Func<double, string> XFormatter { get; set; }

        public Func<double, string> YFormatter { get; set; }

        public VoltageChartViewModel()
        {
            SeriesValues = new ChartValues<double>();
            for (int i = 0; i < 500; i++)
            {
                Random r = new Random(i);
                double value = 5 + r.Next(0, 100) * 0.001;
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
