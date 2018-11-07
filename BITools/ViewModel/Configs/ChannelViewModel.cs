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
    /// <summary>
    /// 通道
    /// </summary>
    public class ChannelViewModel : PropertyNotifyObject
    {
        public ChannelViewModel()
        {
            ChannelInterfaceList = new ObservableCollection<Configs.ChannelInterfaceViewModel>();
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

        public ObservableCollection<ChannelInterfaceViewModel> ChannelInterfaceList
        {
            get { return this.GetValue(c => c.ChannelInterfaceList); }
            set { this.SetValue(c => c.ChannelInterfaceList, value); }
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
                ChannelInterfaceList.Add(ChannelInterfaceViewModel.GetV("1"));
                ChannelInterfaceList.Add(ChannelInterfaceViewModel.GetA("1"));

                ChannelInterfaceList.Add(ChannelInterfaceViewModel.GetV("2"));
                ChannelInterfaceList.Add(ChannelInterfaceViewModel.GetA("2"));

                MontiorParamList.Add(new MonitorParamViewModel { Code = "1", ValType = (int)ValTypeEnum.Selector, Name = "负载模式", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "2", ValType = (int)ValTypeEnum.Input, Name = "负载值(A/V)", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "3", ValType = (int)ValTypeEnum.Input, Name = "启动电压(V)", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "4", ValType = (int)ValTypeEnum.Input, Name = "电压上限(V)", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "5", ValType = (int)ValTypeEnum.Input, Name = "电压下限(V)", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "6", ValType = (int)ValTypeEnum.Input, Name = "电流上限(A)", Val = "" });
                MontiorParamList.Add(new MonitorParamViewModel { Code = "7", ValType = (int)ValTypeEnum.Input, Name = "电流下限(A)", Val = "" });
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
