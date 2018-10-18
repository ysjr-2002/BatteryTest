using BIModel;
using Common.NotifyBase;
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
    public class LiveDataViewModel : PropertyNotifyObject
    {
        public LiveDataViewModel()
        {
            Records = new ObservableCollection<DataRecord>();
            Unknowns = new ObservableCollection<RecordStatus>();
        }

        public ObservableCollection<DataRecord> Records
        {
            get { return this.GetValue(c => c.Records); }
            set { this.SetValue(c => c.Records, value); }
        }

        public ObservableCollection<RecordStatus> Unknowns
        {
            get { return this.GetValue(c => c.Unknowns); }
            set { this.SetValue(c => c.Unknowns, value); }
        }

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
        }
    }
}
