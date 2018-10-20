using BITools.SystemManager;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BITools.ViewModel
{
    public class MainViewModel : BaseMainViewModel
    {
        public int TabSelectedIndex
        {
            get { return this.GetValue(c => c.TabSelectedIndex); }
            set { this.SetValue(c => c.TabSelectedIndex, value); }
        }

        public ICommand TabSelectedCommand { get { return new DelegateCommand<string>(SwitchTab); } }

        public MainViewModel()
        {
            DeviceTestViewModel = new DeviceTestViewModel();
            TabSelectedIndex = 0;
        }
        

        private void SwitchTab(string tab)
        {
            if (tab == "A")
                TabSelectedIndex = 0;
            if (tab == "B")
                TabSelectedIndex = 1;
            if (tab == "C")
                TabSelectedIndex = 2;
            if (tab == "D")
                TabSelectedIndex = 3;
            if (tab == "E")
                TabSelectedIndex = 4;
            if (tab == "F")
                TabSelectedIndex = 5;
        }
    }
}
