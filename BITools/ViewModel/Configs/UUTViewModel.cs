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
using BITools.Enums;

namespace BITools.ViewModel.Configs
{
    /// <summary>
    /// 台车
    /// </summary>
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

        /// <summary>
        /// 通道集合
        /// </summary>
        public ObservableCollection<ChannelViewModel> ChannelList
        {
            get { return this.GetValue(c => c.ChannelList); }
            set { this.SetValue(c => c.ChannelList, value); }
        }

        public void ChangeState()
        {
            var s = (int)(DateTime.Now.Ticks & 0xFFFFFFFF);
            var index = new Random(s).Next(0, 10);
            if (index == 0)
                StateBrush = Application.Current.FindResource("tjColorBrush") as SolidColorBrush;
            else if (index == 1)
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
            var c1 = new ChannelViewModel { Code = "1", Name = "负载通道1", ChannelType = (int)ChannelTypeEnum.FZ };
            var c2 = new ChannelViewModel { Code = "2", Name = "负载通道2", ChannelType = (int)ChannelTypeEnum.FZ };
            c1.Init();
            c2.Init();
            ChannelList.Add(c1);
            ChannelList.Add(c2);

            ChannelList.Add(new ChannelViewModel { Code = "3", Name = "机台通道", ChannelType = (int)ChannelTypeEnum.JT });
            ChannelList.Add(new ChannelViewModel { Code = "4", Name = "温度通道", ChannelType = (int)ChannelTypeEnum.Temperature });
        }
    }
}
