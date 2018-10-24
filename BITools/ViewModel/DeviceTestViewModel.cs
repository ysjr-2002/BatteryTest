using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.ViewModel
{
    /// <summary>
    /// 设备测试
    /// </summary>
    public class DeviceTestViewModel : PropertyNotifyObject
    {
        public DeviceTestViewModel()
        {
            Layer1ViewModel = new LayerViewModel("L1");
            Layer2ViewModel = new LayerViewModel("L2");
            Layer3ViewModel = new LayerViewModel("L3");
        }

        public LayerViewModel Layer1ViewModel
        {
            get { return this.GetValue(c => c.Layer1ViewModel); }
            set { this.SetValue(c => c.Layer1ViewModel, value); }
        }

        public LayerViewModel Layer2ViewModel
        {
            get { return this.GetValue(c => c.Layer2ViewModel); }
            set { this.SetValue(c => c.Layer2ViewModel, value); }
        }

        public LayerViewModel Layer3ViewModel
        {
            get { return this.GetValue(c => c.Layer3ViewModel); }
            set { this.SetValue(c => c.Layer3ViewModel, value); }
        }

    }
}
