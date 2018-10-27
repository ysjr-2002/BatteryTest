using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BITools.Model
{
    public class TabControlCollapseMenuModel : PropertyNotifyObject
    {
        public string Name
        {
            get { return this.GetValue(c => c.Name); }
            set { this.SetValue(c => c.Name, value); }
        }
        public string Header
        {
            get { return this.GetValue(c => c.Header); }
            set { this.SetValue(c => c.Header, value); }
        }

        public TabItem item
        {
            get { return this.GetValue(c => c.item); }
            set { this.SetValue(c => c.item, value); }
        }

        public event EventHandler<EventArgs> RemoveClickEvent = null;
        /// <summary>
        /// 
        /// </summary>
        public ICommand RemoveClickCommand { get { return new DelegateCommand(RemoveClick); } }

        void RemoveClick()
        {
            RemoveClickEvent?.Invoke(this, new EventArgs());
        }

        public event EventHandler<EventArgs> SlectTabItemClickEvent = null;
        public ICommand SlectTabItemClickCommand { get { return new DelegateCommand(SlectTabItemClick); } }

        void SlectTabItemClick()
        {
            SlectTabItemClickEvent?.Invoke(this, new EventArgs());
        }
    }
}
