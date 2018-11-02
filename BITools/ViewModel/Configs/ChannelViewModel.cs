using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using System.Collections.ObjectModel;
using BITools.Enums;

namespace BITools.ViewModel.Configs
{
    public class ChannelViewModel : PropertyNotifyObject
    {
        public ChannelViewModel()
        {
            ChannelSubList = new ObservableCollection<Configs.ChannelSubViewModel>();
            MontiorParamList = new ObservableCollection<Configs.MonitorParamViewModel>();
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

        public int ChannelType
        {
            get { return this.GetValue(c => c.ChannelType); }
            set { this.SetValue(c => c.ChannelType, value); }
        }

        public ObservableCollection<ChannelSubViewModel> ChannelSubList
        {
            get { return this.GetValue(c => c.ChannelSubList); }
            set { this.SetValue(c => c.ChannelSubList, value); }
        }

        public ObservableCollection<MonitorParamViewModel> MontiorParamList
        {
            get { return this.GetValue(c => c.MontiorParamList); }
            set { this.SetValue(c => c.MontiorParamList, value); }
        }

        public void Init()
        {
            if (ChannelType == (int)ChannelTypeEnum.FZ)
            {
                ChannelSubList.Add(ChannelSubViewModel.GetV("1"));
                ChannelSubList.Add(ChannelSubViewModel.GetA("1"));

                ChannelSubList.Add(ChannelSubViewModel.GetV("2"));
                ChannelSubList.Add(ChannelSubViewModel.GetA("2"));

                MontiorParamList.Add(new MonitorParamViewModel { Code = "1", Name = "负载模式", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "2", Name = "负载值", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "3", Name = "启动电压", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "4", Name = "电压上限", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "5", Name = "电压下限", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "6", Name = "电流上限", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "7", Name = "电流下限", Val = "" });
            }
            if (ChannelType == (int)ChannelTypeEnum.JT)
            {

            }
            if (ChannelType == (int)ChannelTypeEnum.Temperature)
            {
                MontiorParamList.Add(new MonitorParamViewModel { Code = "1", Name = "温度上限", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "2", Name = "温度下限", Val = "" });
            }
        }

        //负载通道
        //负载通道
        //机台通道
        //温度设置

        //负载通道下，需要配置电压和电流通讯参数
    }
}
