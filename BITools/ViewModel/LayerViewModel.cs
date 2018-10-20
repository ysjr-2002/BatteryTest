using BITools.DataManager;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BITools.ViewModel
{
    public class LayerViewModel : PropertyNotifyObject
    {
        public LayerViewModel(string name)
        {
            this.Name = name;
            this.TestDataCollection = new ObservableCollection<ViewModel.DeveInfo>();
            this.TestDataCollection.Add(new DeveInfo
            {
                sbbh= name,
                sm = 1,
                lhcsdy = "ss",
                acsr = "0V",
                aczt = "OFF",
                zs = 100,
                hg = 99,
                bl = 1,
                bll = "99%"
            });
        }

        public LayerViewModel()
        {
        }

        public string Name
        {
            get { return this.GetValue(c => c.TDBLCollection); }
            set { this.SetValue(c => c.TDBLCollection, value); }
        }

        public string TDBLCollection
        {
            get { return this.GetValue(c => c.TDBLCollection); }
            set { this.SetValue(c => c.TDBLCollection, value); }
        }

        public ObservableCollection<DeveInfo> TestDataCollection
        {
            get { return this.GetValue(c => c.TestDataCollection); }
            set { this.SetValue(c => c.TestDataCollection, value); }
        }

        public ICommand SDJCCommand { get { return new DelegateCommand(SDJC); } }
        public ICommand KSCSCommand { get { return new DelegateCommand(KSCS); } }
        public ICommand ZTCSCommand { get { return new DelegateCommand(ZTCS); } }
        public ICommand TZCSCommand { get { return new DelegateCommand(TZCS); } }

        public ICommand CKSJCommand { get { return new DelegateCommand(CKSJ); } }
        public ICommand CKTXMCommand { get { return new DelegateCommand(CKTXM); } }
        public ICommand BJFWCommand { get { return new DelegateCommand(BJFW); } }

        /// <summary>
        /// 上电检测
        /// </summary>
        private void SDJC()
        {

        }

        private void KSCS()
        {

        }

        private void ZTCS()
        {

        }

        private void TZCS()
        {

        }

        private void CKSJ()
        {

        }

        private void CKTXM()
        {
            var window = new ProductBarcodeWindow();
            window.ShowDialog();
        }

        private void BJFW()
        {

        }
    }
}
