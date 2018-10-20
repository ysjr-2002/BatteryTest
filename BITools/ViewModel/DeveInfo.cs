using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.ViewModel
{
    public class DeveInfo : PropertyNotifyObject
    {
        public string sbbh
        {
            get { return this.GetValue(c => c.sbbh); }
            set { this.SetValue(c => c.sbbh, value); }
        }

        //扫描
        public int sm
        {
            get { return this.GetValue(c => c.sm); }
            set { this.SetValue(c => c.sm, value); }
        }

        public string lhcsdy
        {
            get { return this.GetValue(c => c.lhcsdy); }
            set { this.SetValue(c => c.lhcsdy, value); }
        }

        //ac输入
        public string acsr
        {
            get { return this.GetValue(c => c.acsr); }
            set { this.SetValue(c => c.acsr, value); }
        }

        public string aczt
        {
            get { return this.GetValue(c => c.aczt); }
            set { this.SetValue(c => c.aczt, value); }
        }

        public int zs
        {
            get { return this.GetValue(c => c.zs); }
            set { this.SetValue(c => c.zs, value); }
        }

        public int hg
        {
            get { return this.GetValue(c => c.hg); }
            set { this.SetValue(c => c.hg, value); }
        }

        public int bl
        {
            get { return this.GetValue(c => c.bl); }
            set { this.SetValue(c => c.bl, value); }
        }

        public string bll
        {
            get { return this.GetValue(c => c.bll); }
            set { this.SetValue(c => c.bll, value); }
        }

        //产品型号
        public string cpxh
        {
            get { return this.GetValue(c => c.cpxh); }
            set { this.SetValue(c => c.cpxh, value); }
        }

        public string cpddh
        {
            get { return this.GetValue(c => c.cpddh); }
            set { this.SetValue(c => c.cpddh, value); }
        }
    }
}
