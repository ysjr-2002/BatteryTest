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
        /// 输出类型 0:电压 1:电流
        /// </summary>
        public int OutputType
        {
            get { return this.GetValue(c => c.OutputType); }
            set { this.SetValue(c => c.OutputType, value); }
        }

        public string Value
        {
            get { return this.GetValue(c => c.Value); }
            set { this.SetValue(c => c.Value, value); }
        }

        public string SX
        {
            get { return this.GetValue(c => c.SX); }
            set { this.SetValue(c => c.SX, value); }
        }

        public string XX
        {
            get { return this.GetValue(c => c.XX); }
            set { this.SetValue(c => c.XX, value); }
        }

        public string Temperature
        {
            get { return this.GetValue(c => c.Temperature); }
            set { this.SetValue(c => c.Temperature, value); }
        }
    }
}
