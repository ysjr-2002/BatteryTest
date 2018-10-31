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

        public int InterfaceType
        {
            get { return this.GetValue(c => c.InterfaceType); }
            set { this.SetValue(c => c.InterfaceType, value); }
        }

        /// <summary>
        /// 输出类型 0:电压 1:电流
        /// </summary>
        public int OutputType
        {
            get { return this.GetValue(c => c.OutputType); }
            set { this.SetValue(c => c.OutputType, value); }
        }
    }
}
