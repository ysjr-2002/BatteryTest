using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.ViewModel.Configs
{
    /// <summary>
    /// 监控参数
    /// </summary>
    public class MonitorParamViewModel : PropertyNotifyObject
    {
        public string Code { get; set; }

        public string Name
        {
            get { return this.GetValue(c => c.Code); }
            set { this.SetValue(c => c.Code, value); }
        }

        /// <summary>
        /// 输入模式
        /// </summary>
        public int InputMode
        {
            get { return this.GetValue(c => c.InputMode); }
            set { this.SetValue(c => c.InputMode, value); }
        }

        /// <summary>
        /// 值类型(电压、电流、温度etc)
        /// </summary>
        public int ValType
        {
            get { return this.GetValue(c => c.ValType); }
            set { this.SetValue(c => c.ValType, value); }
        }

        public string Val
        {
            get { return this.GetValue(c => c.Val); }
            set { this.SetValue(c => c.Val, value); }
        }
    }
}
