using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;

namespace BITools.ViewModel
{
    public class AgingParamViewModel : BaseViewModel
    {
        public AgingParamViewModel()
        {
            this.JiaRe = "10";
            this.ShaoJi = "10";
            this.PaiFeng = "10";
            this.BaoJing = "10";
        }

        public string JiaRe
        {
            get { return this.GetValue(c => c.JiaRe); }
            set { this.SetValue(c => c.JiaRe, value); }
        }

        public string ShaoJi
        {
            get { return this.GetValue(c => c.ShaoJi); }
            set { this.SetValue(c => c.ShaoJi, value); }
        }

        public string PaiFeng
        {
            get { return this.GetValue(c => c.PaiFeng); }
            set { this.SetValue(c => c.PaiFeng, value); }
        }

        public string BaoJing
        {
            get { return this.GetValue(c => c.BaoJing); }
            set { this.SetValue(c => c.BaoJing, value); }
        }

        protected override void Loaded()
        {

        }

        protected override void Save()
        {

        }

        protected override void Cancel()
        {
            
        }
    }
}
