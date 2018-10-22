using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.ViewModel
{
    /// <summary>
    /// 通道信息
    /// </summary>
    public class ChannelInfo : PropertyNotifyObject
    {
        public string csms
        {
            get { return this.GetValue(c => c.csms); }
            set { this.SetValue(c => c.csms, value); }
        }

        public string sdcs1
        {
            get { return this.GetValue(c => c.sdcs1); }
            set { this.SetValue(c => c.sdcs1, value); }
        }

        public string sdcs2
        {
            get { return this.GetValue(c => c.sdcs2); }
            set { this.SetValue(c => c.sdcs2, value); }
        }

        public string sdcs3
        {
            get { return this.GetValue(c => c.sdcs3); }
            set { this.SetValue(c => c.sdcs3, value); }
        }

        public string qddy
        {
            get { return this.GetValue(c => c.qddy); }
            set { this.SetValue(c => c.qddy, value); }
        }

        public string csxx1
        {
            get { return this.GetValue(c => c.csxx1); }
            set { this.SetValue(c => c.csxx1, value); }
        }

        public string cssx1
        {
            get { return this.GetValue(c => c.cssx1); }
            set { this.SetValue(c => c.cssx1, value); }
        }

        public string csxx2
        {
            get { return this.GetValue(c => c.csxx2); }
            set { this.SetValue(c => c.csxx2, value); }
        }

        public string cssx2
        {
            get { return this.GetValue(c => c.cssx2); }
            set { this.SetValue(c => c.cssx2, value); }
        }

        public string xlkz
        {
            get { return this.GetValue(c => c.xlkz); }
            set { this.SetValue(c => c.xlkz, value); }
        }

        public ChannelInfo()
        {
            csms = "1-CC模式";
            sdcs1 = "1.000A";
            sdcs2 = "3.000A";
            sdcs3 = "5.000A";
            qddy = "0.00";

            csxx1 = "11.500A";
            cssx1 = "13.500A";
            csxx2 = "0.00";
            cssx2 = "0.00";
            xlkz = "0.00";
        }
    }
}
