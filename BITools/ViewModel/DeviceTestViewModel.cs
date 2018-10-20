using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.ViewModel
{
    public class DeviceTestViewModel : PropertyNotifyObject
    {
        public DeviceTestViewModel()
        {
            Layer1ViewModel = new LayerViewModel("");
            Layer2ViewModel = new LayerViewModel("");
            Layer3ViewModel = new LayerViewModel("");
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
