using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.ViewModel.GH
{
    /// <summary>
    /// 每层的设备信息
    /// </summary>
    public class GHGroup : PropertyNotifyObject
    {
        public GHGroup(string code1, string code2)
        {
            L1Code = code1;
            L2Code = code2;
        }

        public string L1Code
        {
            get { return this.GetValue(c => c.L1Code); }
            set { this.SetValue(c => c.L1Code, value); }
        }

        public string L2Code
        {
            get { return this.GetValue(c => c.L2Code); }
            set { this.SetValue(c => c.L2Code, value); }
        }
    }
}
