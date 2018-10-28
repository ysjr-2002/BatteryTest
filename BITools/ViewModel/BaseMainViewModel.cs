using BITools.DataManager;
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
    public class BaseMainViewModel : BaseViewModel
    {
        public ICommand LoginUserCommand { get { return new DelegateCommand(LoginUser); } }
        public ICommand UserManagerCommand { get { return new DelegateCommand(UserManager); } }
        public ICommand SystemSettingCommand { get { return new DelegateCommand(SystemSetting); } }
        public ICommand SystemParamCommand { get { return new DelegateCommand(SystemParam); } }
        public ICommand ReportCommand { get { return new DelegateCommand(Report); } }
        public ICommand AboutCommand { get { return new DelegateCommand(About); } }
        public ICommand DeviceConfigCommand { get { return new DelegateCommand(DeviceConfig); } }

        
        public TCViewModel DeviceTestViewModelA
        {
            get { return this.GetValue(c => c.DeviceTestViewModelA); }
            set { this.SetValue(c => c.DeviceTestViewModelA, value); }
        }

        public TCViewModel DeviceTestViewModelB
        {
            get { return this.GetValue(c => c.DeviceTestViewModelB); }
            set { this.SetValue(c => c.DeviceTestViewModelB, value); }
        }

        public TCViewModel DeviceTestViewModelC
        {
            get { return this.GetValue(c => c.DeviceTestViewModelC); }
            set { this.SetValue(c => c.DeviceTestViewModelC, value); }
        }

        public TCViewModel DeviceTestViewModelD
        {
            get { return this.GetValue(c => c.DeviceTestViewModelD); }
            set { this.SetValue(c => c.DeviceTestViewModelD, value); }
        }

        public TCViewModel DeviceTestViewModelE
        {
            get { return this.GetValue(c => c.DeviceTestViewModelE); }
            set { this.SetValue(c => c.DeviceTestViewModelE, value); }
        }

        public TCViewModel DeviceTestViewModelF
        {
            get { return this.GetValue(c => c.DeviceTestViewModelF); }
            set { this.SetValue(c => c.DeviceTestViewModelF, value); }
        }

        private void LoginUser()
        {
            var window = new PassWordWindow();
            window.ShowDialog();
        }

        private void UserManager()
        {
            var window = new UserWindow();
            window.ShowDialog();
        }

        private void SystemSetting()
        {
            var window = new SettingWindow();
            window.ShowDialog();
        }

        private void SystemParam()
        {
            var window = new DeviceParamWindow();
            window.ShowDialog();
        }

        private void Report()
        {
            var window = new TCRecordDataWindow();
            window.ShowDialog();
        }

        private void About()
        {
            var window = new AboutWindow();
            window.ShowDialog();
        }

        private void DeviceConfig()
        {
            var window = new DeviceWindow();
            window.ShowDialog();
        }
    }
}
