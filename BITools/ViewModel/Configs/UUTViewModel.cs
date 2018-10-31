using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using BITools.Model;

namespace BITools.ViewModel.Configs
{
    public class UUTViewModel : PropertyNotifyObject
    {
        public UUTViewModel()
        {
            ChannelList = new ObservableCollection<ChannelViewModel>();
            StateBrush = Application.Current.FindResource("tjColorBrush") as SolidColorBrush;
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

        public void ChangeState()
        {
            var s = (int)(DateTime.Now.Ticks & 0xFFFFFFFF);
            var index = new Random(s).Next(0, 10);
            if( index == 0)
                StateBrush = Application.Current.FindResource("tjColorBrush") as SolidColorBrush;
            else if( index == 1)
                StateBrush = Application.Current.FindResource("lzycColorBrush") as SolidColorBrush;
            else if (index == 2)
                StateBrush = Application.Current.FindResource("wcpColorBrush") as SolidColorBrush;
            else if (index == 3)
                StateBrush = Application.Current.FindResource("qyColorBrush") as SolidColorBrush;
            else if (index == 4)
                StateBrush = Application.Current.FindResource("qlColorBrush") as SolidColorBrush;
            else if (index == 5)
                StateBrush = Application.Current.FindResource("gyColorBrush") as SolidColorBrush;
            else if (index == 6)
                StateBrush = Application.Current.FindResource("glColorBrush") as SolidColorBrush;
            else if (index == 7)
                StateBrush = Application.Current.FindResource("wscColorBrush") as SolidColorBrush;
            else if (index == 8)
                StateBrush = Application.Current.FindResource("hgColorBrush") as SolidColorBrush;
            else if (index == 9)
                StateBrush = Application.Current.FindResource("fzbhColorBrush") as SolidColorBrush;
        }

        public void Init()
        {
            ChannelList.Add(new ChannelViewModel { Code = "1", OutputType = (int)(OutputEnum.V) });
            ChannelList.Add(new ChannelViewModel { Code = "1", OutputType = (int)(OutputEnum.A) });

            ChannelList.Add(new ChannelViewModel { Code = "2", OutputType = (int)(OutputEnum.V) });
            ChannelList.Add(new ChannelViewModel { Code = "2", OutputType = (int)(OutputEnum.A) });

            ChannelList.Add(new ChannelViewModel { Code = "3", OutputType = (int)(OutputEnum.V) });
            ChannelList.Add(new ChannelViewModel { Code = "3", OutputType = (int)(OutputEnum.A) });

            ChannelList.Add(new ChannelViewModel { Code = "4", OutputType = (int)(OutputEnum.T) });
        }
    }
}
