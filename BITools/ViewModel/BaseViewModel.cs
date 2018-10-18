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

        protected abstract void Loaded();
    }
}
