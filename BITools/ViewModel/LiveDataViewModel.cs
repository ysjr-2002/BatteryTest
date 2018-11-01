using BIDataAccess.entities;
using BILogic;
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
    public class LiveDataViewModel : BaseViewModel
    {
        public LiveDataViewModel()
        {
            TCDataHistoryCollection = new ObservableCollection<TCRecord>();
            Records = new ObservableCollection<DataRecord>();
            Unknowns = new ObservableCollection<RecordStatus>();

            StartDateTime = DateTime.Now.Date.ToStandard();
            EndDateTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToStandard();

            VSeries = new SeriesCollection();
            ASeries = new SeriesCollection();
            VLables = new ObservableCollection<string>();
            ALables = new ObservableCollection<string>();

            Formatter = ConvertValue;
        }

        public SeriesCollection VSeries
        {
            get { return this.GetValue(c => c.VSeries); }
            set { this.SetValue(c => c.VSeries, value); }
        }

        public SeriesCollection ASeries
        {
            get { return this.GetValue(c => c.ASeries); }
            set { this.SetValue(c => c.ASeries, value); }
        }

        /// <summary>
        /// X轴
        /// </summary>
        public ObservableCollection<string> VLables
        {
            get { return this.GetValue(c => c.VLables); }
            set { this.SetValue(c => c.VLables, value); }
        }

        public ObservableCollection<string> ALables
        {
            get { return this.GetValue(c => c.ALables); }
            set { this.SetValue(c => c.ALables, value); }
        }

        public ObservableCollection<UserInfo> UserCollection
        {
            get { return this.GetValue(c => c.UserCollection); }
            set { this.SetValue(c => c.UserCollection, value); }
        }

        public List<Dictonary> QujianCollection
        {
            get { return this.GetValue(c => c.QujianCollection); }
            set { this.SetValue(c => c.QujianCollection, value); }
        }

        /// <summary>
        /// 台车老化历史记录
        /// </summary>
        public ObservableCollection<TCRecord> TCDataHistoryCollection
        {
            get { return this.GetValue(c => c.TCDataHistoryCollection); }
            set { this.SetValue(c => c.TCDataHistoryCollection, value); }
        }

        public ObservableCollection<DataRecord> Records
        {
            get { return this.GetValue(c => c.Records); }
            set { this.SetValue(c => c.Records, value); }
        }

        public UserInfo SelectedUser
        {
            get { return this.GetValue(c => c.SelectedUser); }
            set { this.SetValue(c => c.SelectedUser, value); }
        }

        public Dictonary SelectedQujian
        {
            get { return this.GetValue(c => c.SelectedQujian); }
            set { this.SetValue(c => c.SelectedQujian, value); }
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
        public ICommand ExportCommand { get { return new DelegateCommand(Export); } }

        public override void Loaded()
        {
            base.Loaded();
            var temp = new UserService().getUser();
            temp.Insert(0, new UserInfo { UserID = 0, UserName = "全部" });
            UserCollection = new ObservableCollection<UserInfo>(temp);
            SelectedUser = UserCollection.First();

            QujianCollection = FunExt.GetQuJian();
            SelectedQujian = QujianCollection.FirstOrDefault();
        }

        private void QueryData()
        {
            TCDataHistoryCollection.Add(new TCRecord { cpdhh = "343434343", jx = "L-23232", qj = "L2", lhzsj = "23:32:32", czy = "admin", path = "c:\\data.xls" });

            Records.Add(new DataRecord { Time = "12:22:00", Input = "220", X = "℃", Voltage = "23", Ammeter = "34" });

            Unknowns.Clear();
            Unknowns.Add(new RecordStatus { Index = 1, Status = "未连接" });
            Unknowns.Add(new RecordStatus { Index = 2, Status = "未连接" });
            Unknowns.Add(new RecordStatus { Index = 3, Status = "未连接" });
            Unknowns.Add(new RecordStatus { Index = 4, Status = "未连接" });
            Unknowns.Add(new RecordStatus { Index = 5, Status = "未连接" });

            var line1 = new LineSeries();
            line1.Values = new ChartValues<double>();
            line1.Values.Add(4.0);
            line1.Values.Add(15.0);
            line1.Values.Add(23.0);
            line1.Values.Add(5.0);
            line1.Values.Add(7.0);
            line1.Values.Add(9.0);
            line1.Values.Add(12.0);
            VSeries.Add(line1);

            VLables.Add("1.0");
            VLables.Add("2.0");
            VLables.Add("3.0");
            VLables.Add("4.0");
            VLables.Add("5.0");
            VLables.Add("6.0");
            VLables.Add("7.0");
            VLables.Add("8.0");

            var line2 = new LineSeries();
            line2.Values = new ChartValues<double>();
            line2.Values.Add(12.0);
            line2.Values.Add(8.0);
            line2.Values.Add(2.0);
            line2.Values.Add(10.0);
            line2.Values.Add(4.0);
            line2.Values.Add(9.0);
            line2.Values.Add(5.0);
            ASeries.Add(line2);

            ALables.Add("1.0");
            ALables.Add("2.0");
            ALables.Add("3.0");
            ALables.Add("4.0");
            ALables.Add("5.0");
            ALables.Add("6.0");
            ALables.Add("7.0");
            ALables.Add("8.0");
        }

        private string ConvertValue(double value)
        {
            // Console.WriteLine(value.ToString());
            var val = Convert.ToInt32(value).ToString();// + "k";
            return val;
        }

        private void Export()
        {
            Excel.ExcelHelper.ExportStat_CLPPTJ(null);
        }
    }
}
