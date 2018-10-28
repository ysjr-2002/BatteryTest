using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using System.Collections.ObjectModel;

namespace BITools.ViewModel.Configs
{
    public class LayerViewModel : PropertyNotifyObject
    {
        public LayerViewModel()
        {
            UUTList = new ObservableCollection<UUTViewModel>();
        }

        public string Code
        {
            get { return this.GetValue(c => c.Code); }
            set { this.SetValue(c => c.Code, value); }
        }

        public string Name
        {
            get { return this.GetValue(c => c.Name); }
            set { this.SetValue(c => c.Name, value); }
        }

        public string LHSJ
        {
            get { return this.GetValue(c => c.LHSJ); }
            set { this.SetValue(c => c.LHSJ, value); }
        }

        public ObservableCollection<UUTViewModel> UUTList
        {
            get { return this.GetValue(c => c.UUTList); }
            set { this.SetValue(c => c.UUTList, value); }
        }
    }
}
