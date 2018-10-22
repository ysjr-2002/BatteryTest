using BIModel;
using Common.NotifyBase;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BITools.ViewModel
{
    /// <summary>
    /// 历史数据查询
    /// </summary>
    public class LiveDataViewModel : PropertyNotifyObject
    {
        public LiveDataViewModel()
        {
            Records = new ObservableCollection<DataRecord>();
            Unknowns = new ObservableCollection<RecordStatus>();

            StartDateTime = DateTime.Now.Date.ToStandard();
            EndDateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToStandard();

            Series = new SeriesCollection();
            Lables = new ObservableCollection<string>();

            Formatter = ConvertValue;
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

        public ObservableCollection<DataRecord> Records
        {
            get { return this.GetValue(c => c.Records); }
            set { this.SetValue(c => c.Records, value); }
        }

        public string StartDateTime
        {
            get { return this.GetValue(c => c.StartDateTime); }
            set { this.SetValue(c => c.StartDateTime, value); }
        }

        public string EndDateTime
        {
            get { return this.GetValue(c => c.EndDateTime); }
            set { this.SetValue(c => c.EndDateTime, value); }
        }

        public ObservableCollection<RecordStatus> Unknowns
        {
            get { return this.GetValue(c => c.Unknowns); }
            set { this.SetValue(c => c.Unknowns, value); }
        }

        public Func<double, string> Formatter { get; set; }

        public ICommand QueryCommand { get { return new DelegateCommand(QueryData); } }

        private void QueryData()
        {
            Records.Add(new DataRecord { Time = "12:22:00", Input = "220", X = "℃", Voltage = "23", Ammeter = "34" });

            Unknowns.Clear();
            Unknowns.Add(new RecordStatus { Index = 1, Status = "未连接" });
            Unknowns.Add(new RecordStatus { Index = 2, Status = "未连接" });
            Unknowns.Add(new RecordStatus { Index = 3, Status = "未连接" });
            Unknowns.Add(new RecordStatus { Index = 4, Status = "未连接" });
            Unknowns.Add(new RecordStatus { Index = 5, Status = "未连接" });

            var line1 = new LineSeries();
            line1.Values = new ChartValues<double>();
            line1.Values.Add(0.25);
            line1.Values.Add(0.50);
            line1.Values.Add(0.75);
            line1.Values.Add(0.50);
            line1.Values.Add(1.00);
            line1.Values.Add(1.25);
            line1.Values.Add(0.75);
            Series.Add(line1);

            Lables.Add("0");
            Lables.Add("119.0");
            Lables.Add("219.0");
            Lables.Add("319.0");
            Lables.Add("419.0");
            Lables.Add("519.0");
            Lables.Add("619.0");
            Lables.Add("719.0");
            Lables.Add("819.0");
        }

        private string ConvertValue(double value)
        {
            // Console.WriteLine(value.ToString());
            return Convert.ToInt32(value).ToString();// + "k";
        }
    }
}
