using BITools.ViewModel;
using Common.NotifyBase;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BITools
{
    class ChartTestViewModel : BaseViewModel
    {
        public ChartTestViewModel()
        {
            Series = new SeriesCollection();
            Lables = new ObservableCollection<string>();
            XFormatter = ConvertValueX;
            YFormatter = ConvertValueY;
        }

        public SeriesCollection Series
        {
            get { return this.GetValue(c => c.Series); }
            set { this.SetValue(c => c.Series, value); }
        }

        /// <summary>
        /// X轴
        /// </summary>
        public ObservableCollection<string> Lables
        {
            get { return this.GetValue(c => c.Lables); }
            set { this.SetValue(c => c.Lables, value); }
        }

        public Func<int, string> XFormatter { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public override void Loaded()
        {
            base.Loaded();

            for (int i = 1; i <= 24; i++)
            {
                Lables.Add(string.Format("{0:D2}", i));
            }

            var line1 = new LineSeries();
            line1.Values = new ChartValues<double>();
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            line1.Values.Add(23.0);
            Series.Add(line1);


            Task.Factory.StartNew(() =>
            {
                var i = 25;
                while (true)
                {
                    //Lables.Add(i.ToString());
                    line1.Values.Add((double)(new Random().Next(22, 25)));
                    Lables.Add(i.ToString());

                    line1.Values.RemoveAt(0);
                    Lables.RemoveAt(0);
                    Thread.Sleep(1000);
                }
            });
        }

        private string ConvertValueY(double value)
        {
            // Console.WriteLine(value.ToString());
            var val = Convert.ToInt32(value).ToString();// + "k";
            return val;
        }

        private string ConvertValueX(int value)
        {
            return value.ToString();
        }
    }
}
