using BIModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;

namespace BITools.ViewModel
{
    /// <summary>
    /// 系统设置ViewModel
    /// </summary>
    public class SettingViewModel : BaseViewModel
    {
        public SettingViewModel()
        {
        }

        public ObservableCollection<string> ComPortCollection
        {
            get { return this.GetValue(c => c.ComPortCollection); }
            set { this.SetValue(c => c.ComPortCollection, value); }
        }

        public ICommand CreateCommand { get { return new DelegateCommand(Save); } }

        protected override void Loaded()
        {
            ComPortCollection = new ObservableCollection<string>(FunExt.GetSerialPorts());
        }

        private void Save()
        {

        }
    }
}
