using BITools.Enums;
using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.ViewModel.Configs
{
    /// <summary>
    /// 通道通讯接口
    /// </summary>
    public class ChannelInterfaceViewModel : PropertyNotifyObject
    {
        public ChannelInterfaceViewModel()
        {
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

        public static ChannelInterfaceViewModel GetV(string code)
        {
            var temp = new ChannelInterfaceViewModel();
            temp.Code = code;
            temp.Name = "电压";
            temp.OutputType = (int)OutputEnum.V;
            return temp;
        }

        public static ChannelInterfaceViewModel GetA(string code)
        {
            var temp = new ChannelInterfaceViewModel();
            temp.Code = code;
            temp.Name = "电流";
            temp.OutputType = (int)OutputEnum.A;
            return temp;
        }
    }
}
