using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using System.Collections.ObjectModel;

namespace BITools.ViewModel.Configs
{
    /// <summary>
    /// 台车
    /// </summary>
    public class TCViewModel : PropertyNotifyObject
    {
        public TCViewModel()
        {
            LayerList = new ObservableCollection<LayerViewModel>();
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

        public bool IsEnable
        {
            get { return this.GetValue(c => c.IsEnable); }
            set { this.SetValue(c => c.IsEnable, value); }
        }

        public ObservableCollection<LayerViewModel> LayerList
        {
            get { return this.GetValue(c => c.LayerList); }
            set { this.SetValue(c => c.LayerList, value); }
        }
    }
}
