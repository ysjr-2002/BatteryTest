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
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

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
            StateBrush = "/BITools;component/Images/Status/dj.png";
        }

        public string Code
        {
            get { return this.GetValue(c => c.Code); }
            set { this.SetValue(c => c.Code, value); }
        }

        [JsonIgnore]
        public string StateBrush
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
                StateBrush = "/BITools;component/Images/Status/dj.png";
            else if (index == 1)
                StateBrush = "/BITools;component/Images/Status/lzyc.png";
            else if (index == 2)
                StateBrush = "/BITools;component/Images/Status/wcp.png";
            else if (index == 3)
                StateBrush = "/BITools;component/Images/Status/qy.png";
            else if (index == 4)
                StateBrush = "/BITools;component/Images/Status/ql.png";
            else if (index == 5)
                StateBrush = "/BITools;component/Images/Status/gy.png";
            else if (index == 6)
                StateBrush = "/BITools;component/Images/Status/gl.png";
            else if (index == 7)
                StateBrush = "/BITools;component/Images/Status/wsc.png";
            else if (index == 8)
                StateBrush = "/BITools;component/Images/Status/hg.png";
            else if (index == 9)
                StateBrush = "/BITools;component/Images/Status/fzbh.png";
        }

        public void Init()
        {
            var c1 = new ChannelViewModel { Code = "1", Name = "负载通道1", ChannelType = (int)ChannelTypeEnum.FZ };
            var c2 = new ChannelViewModel { Code = "2", Name = "负载通道2", ChannelType = (int)ChannelTypeEnum.FZ };
            var c3 = new ChannelViewModel { Code = "3", Name = "机台通道", ChannelType = (int)ChannelTypeEnum.JT };
            var c4 = new ChannelViewModel { Code = "4", Name = "温度通道", ChannelType = (int)ChannelTypeEnum.Temperature };
            c1.Init(1);
            c2.Init(2);
            ChannelList.Add(c1);
            ChannelList.Add(c2);
            ChannelList.Add(c3);
            ChannelList.Add(c4);
        }
    }
}
