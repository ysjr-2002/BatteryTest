using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;

namespace BITools.ViewModel.Configs
{
    public class UUTViewModel : PropertyNotifyObject
    {
        public UUTViewModel()
        {
            ChannelList = new ObservableCollection<ChannelViewModel>();
            StateBrush = Application.Current.FindResource("lzycColorBrush") as SolidColorBrush;
        }

        public string Code
        {
            get { return this.GetValue(c => c.Code); }
            set { this.SetValue(c => c.Code, value); }
        }

        public Brush StateBrush
        {
            get { return this.GetValue(c => c.StateBrush); }
            set { this.SetValue(c => c.StateBrush, value); }
        }

        public ObservableCollection<ChannelViewModel> ChannelList
        {
            get { return this.GetValue(c => c.ChannelList); }
            set { this.SetValue(c => c.ChannelList, value); }
        }
    }
}
