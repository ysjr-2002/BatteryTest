using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using System.Collections.ObjectModel;

namespace BITools.ViewModel.Configs
{
    public class ChannelViewModel : PropertyNotifyObject
    {
        public string Code
        {
            get { return this.GetValue(c => c.Code); }
            set { this.SetValue(c => c.Code, value); }
        }

        /// <summary>
        /// 输出类型 0:电压 1:电流 2:温度
        /// </summary>
        public int OutputType
        {
            get { return this.GetValue(c => c.OutputType); }
            set { this.SetValue(c => c.OutputType, value); }
        }

        public int InterfaceType
        {
            get { return this.GetValue(c => c.InterfaceType); }
            set { this.SetValue(c => c.InterfaceType, value); }
        }

        public string Address
        {
            get { return this.GetValue(c => c.Address); }
            set { this.SetValue(c => c.Address, value); }
        }

        //负载通道
        //负载通道
        //机台通道
        //温度设置

        //负载通道下，需要配置电压和电流通讯参数
    }
}
