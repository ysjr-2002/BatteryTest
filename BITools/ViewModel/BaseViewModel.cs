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
    public abstract class BaseViewModel : PropertyNotifyObject
    {
        public ICommand LoadedCommand { get { return new DelegateCommand(Loaded); } }
        public ICommand SaveCommand { get { return new DelegateCommand(Save); } }
        public ICommand CancelCommand { get { return new DelegateCommand(Cancel); } }

        protected abstract void Loaded();
        protected virtual void Save()
        {
        }

        protected virtual void Cancel()
        {

        }
    }
}
