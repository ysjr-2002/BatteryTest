using BITools.Model;
using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.Core
{
    public class DeviceParamJson : PropertyNotifyObject
    {
        public string JiaRe
        {
            get { return this.GetValue(c => c.JiaRe); }
            set { this.SetValue(c => c.JiaRe, value); }
        }

        public string ShaoJi
        {
            get { return this.GetValue(c => c.ShaoJi); }
            set { this.SetValue(c => c.ShaoJi, value); }
        }

        public string PaiFeng
        {
            get { return this.GetValue(c => c.PaiFeng); }
            set { this.SetValue(c => c.PaiFeng, value); }
        }

        public string BaoJing
        {
            get { return this.GetValue(c => c.BaoJing); }
            set { this.SetValue(c => c.BaoJing, value); }
        }

        /// <summary>
        /// 循环次数
        /// </summary>
        public string XHCS
        {
            get { return this.GetValue(c => c.XHCS); }
            set { this.SetValue(c => c.XHCS, value); }
        }

        /// <summary>
        /// 老化时序集合
        /// </summary>
        public ObservableCollection<LHSXInfo> LHSXCollection
        {
            get { return this.GetValue(c => c.LHSXCollection); }
            set { this.SetValue(c => c.LHSXCollection, value); }
        }



    }
}
