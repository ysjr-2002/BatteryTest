using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using System.Collections.ObjectModel;

namespace BITools.ViewModel.Configs
{
    public class UUTViewModel : PropertyNotifyObject
    {
        public UUTViewModel()
        {
            ChannelList = new ObservableCollection<ChannelViewModel>();
        }

        public string Code
        {
            get { return this.GetValue(c => c.Code); }
            set { this.SetValue(c => c.Code, value); }
        }

        public ObservableCollection<ChannelViewModel> ChannelList
        {
            get { return this.GetValue(c => c.ChannelList); }
            set { this.SetValue(c => c.ChannelList, value); }
        }
    }
}
