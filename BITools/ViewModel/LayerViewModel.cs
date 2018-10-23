using BIModel;
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
                sbbh = name,
                sm = 1,
                lhcsdy = "ss",
                acsr = "0V",
                aczt = "OFF",
                zs = 100,
                hg = 99,
                bl = 1,
                bll = "99%"
            });
            lhsjEnum = LHSJEnum.LHZSJ;
            LHSJName = FunExt.GetDescription(lhsjEnum);
        }

        public LayerViewModel()
        {
        }

        public string Name
        {
            get { return this.GetValue(c => c.TDBLCollection); }
            set { this.SetValue(c => c.TDBLCollection, value); }
        }

        public string LHSJName
        {
            get { return this.GetValue(c => c.LHSJName); }
            set { this.SetValue(c => c.LHSJName, value); }
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

        private LHSJEnum lhsjEnum;
        public ICommand LHSJCommand { get { return new DelegateCommand(LHSJ); } }

        public ICommand SDJCCommand { get { return new DelegateCommand(SDJC); } }
        public ICommand KSCSCommand { get { return new DelegateCommand(KSCS); } }
        public ICommand ZTCSCommand { get { return new DelegateCommand(ZTCS); } }
        public ICommand TZCSCommand { get { return new DelegateCommand(TZCS); } }

        public ICommand CKSJCommand { get { return new DelegateCommand(CKSJ); } }
        public ICommand CKTXMCommand { get { return new DelegateCommand(CKTXM); } }
        public ICommand BJFWCommand { get { return new DelegateCommand(BJFW); } }

        private void LHSJ()
        {
            if (lhsjEnum == LHSJEnum.LHZSJ)
            {
                lhsjEnum = LHSJEnum.KSSJ;
            }
            else if (lhsjEnum == LHSJEnum.KSSJ)
            {
                lhsjEnum = LHSJEnum.JSSJ;
            }
            else if (lhsjEnum == LHSJEnum.JSSJ)
            {
                lhsjEnum = LHSJEnum.LHZSJ;
            }
            LHSJName = FunExt.GetDescription(lhsjEnum);
        }

        /// <summary>
        /// 上电检测
        /// </summary>
        private void SDJC()
        {

        }

        /// <summary>
        /// 开始测试
        /// </summary>
        private void KSCS()
        {

        }

        /// <summary>
        /// 暂停测试
        /// </summary>
        private void ZTCS()
        {

        }

        /// <summary>
        /// 停止测试
        /// </summary>
        private void TZCS()
        {

        }

        /// <summary>
        /// 查看数据
        /// </summary>
        private void CKSJ()
        {

        }

        /// <summary>
        /// 产品条形码
        /// </summary>
        private void CKTXM()
        {
            var window = new ProductBarcodeWindow();
            window.ShowDialog();
        }

        /// <summary>
        /// 报警复位
        /// </summary>
        private void BJFW()
        {

        }

        private void SwitchTime()
        {
            //老化时间
            //开始时间
            //结束时间
        }
    }
}
