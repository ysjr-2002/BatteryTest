using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;

namespace BITools.ViewModel
{
    public class DataSaveViewModel : BaseViewModel
    {
        public DataSaveViewModel()
        {
            DateSaveTimeSpan = "60";
        }

        public string DateSaveTimeSpan
        {
            get { return this.GetValue(c => c.DateSaveTimeSpan); }
            set { this.SetValue(c => c.DateSaveTimeSpan, value); }
        }

        public override void Loaded()
        {
            base.Loaded();
        }
    }
}
