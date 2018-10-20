﻿using BITools.SystemManager;
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

        public DeviceTestViewModel DeviceTestViewModel
        {
            get { return this.GetValue(c => c.DeviceTestViewModel); }
            set { this.SetValue(c => c.DeviceTestViewModel, value); }
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

        }

        private void About()
        {
            var window = new AboutWindow();
            window.ShowDialog();
        }
    }
}
