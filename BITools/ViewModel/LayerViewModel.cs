using BIModel;
using BITools.Core;
using BITools.DataManager;
using BITools.Helpers;
using BITools.ViewModel.GH;
using Common.NotifyBase;
using LL.SenicSpot.Gate.Kernal;
using Microsoft.Practices.Prism.Commands;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BITools.ViewModel
{
    /// <summary>
    /// 一个台车可以有6层
    /// 每层可以放6台设备
    /// </summary>
    public class LayerViewModel : PropertyNotifyObject
    {
        public LayerViewModel(string name)
        {
            this.Name = name;
            this.TestDataCollection = new ObservableCollection<DeveInfo>();
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

            GHCollection = new ObservableCollection<GHGroup>();

            InitGHGroup();
        }

        private void InitGHGroup()
        {
            GHGroup1 = new GHGroup("1", "2");
            GHGroup2 = new GHGroup("3", "4");
            GHGroup3 = new GHGroup("5", "6");
            GHGroup4 = new GHGroup("7", "8");
            GHGroup5 = new GHGroup("9", "10");
            GHGroup6 = new GHGroup("11", "12");
            GHGroup7 = new GHGroup("13", "14");
            GHGroup8 = new GHGroup("15", "16");
        }

        public Config config
        {
            get
            {
                return NinjectKernal.Instance.Get<Config>();
            }
        }

        public string Name
        {
            get { return this.GetValue(c => c.Name); }
            set { this.SetValue(c => c.Name, value); }
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

        public GHGroup GHGroup1
        {
            get { return this.GetValue(c => c.GHGroup1); }
            set { this.SetValue(c => c.GHGroup1, value); }
        }

        public GHGroup GHGroup2
        {
            get { return this.GetValue(c => c.GHGroup2); }
            set { this.SetValue(c => c.GHGroup2, value); }
        }

        public GHGroup GHGroup3
        {
            get { return this.GetValue(c => c.GHGroup3); }
            set { this.SetValue(c => c.GHGroup3, value); }
        }

        public GHGroup GHGroup4
        {
            get { return this.GetValue(c => c.GHGroup4); }
            set { this.SetValue(c => c.GHGroup4, value); }
        }

        public GHGroup GHGroup5
        {
            get { return this.GetValue(c => c.GHGroup5); }
            set { this.SetValue(c => c.GHGroup5, value); }
        }

        public GHGroup GHGroup6
        {
            get { return this.GetValue(c => c.GHGroup6); }
            set { this.SetValue(c => c.GHGroup6, value); }
        }

        public GHGroup GHGroup7
        {
            get { return this.GetValue(c => c.GHGroup7); }
            set { this.SetValue(c => c.GHGroup7, value); }
        }

        public GHGroup GHGroup8
        {
            get { return this.GetValue(c => c.GHGroup8); }
            set { this.SetValue(c => c.GHGroup8, value); }
        }

        public ObservableCollection<DeveInfo> TestDataCollection
        {
            get { return this.GetValue(c => c.TestDataCollection); }
            set { this.SetValue(c => c.TestDataCollection, value); }
        }

        public ObservableCollection<GHGroup> GHCollection
        {
            get { return this.GetValue(c => c.GHCollection); }
            set { this.SetValue(c => c.GHCollection, value); }
        }

        /// <summary>
        /// 老化总时间
        /// </summary>
        public int Lhzsj
        {
            get { return this.GetValue(c => c.Lhzsj); }
            set { this.SetValue(c => c.Lhzsj, value); }
        }

        /// <summary>
        /// 老化时间
        /// </summary>
        public int Lhsj
        {
            get { return this.GetValue(c => c.Lhsj); }
            set { this.SetValue(c => c.Lhsj, value); }
        }

        //脱机
        public bool Istj
        {
            get { return this.GetValue(c => c.Istj); }
            set { this.SetValue(c => c.Istj, value); }
        }

        //下载参数中
        public bool Isxzczz
        {
            get { return this.GetValue(c => c.Isxzczz); }
            set { this.SetValue(c => c.Isxzczz, value); }
        }

        //停止拉载
        public bool Istzlz
        {
            get { return this.GetValue(c => c.Istzlz); }
            set { this.SetValue(c => c.Istzlz, value); }
        }

        //拉载异常
        public bool Islzyc
        {
            get { return this.GetValue(c => c.Islzyc); }
            set { this.SetValue(c => c.Islzyc, value); }
        }

        //无产品
        public bool Iswcp
        {
            get { return this.GetValue(c => c.Iswcp); }
            set { this.SetValue(c => c.Iswcp, value); }
        }

        //欠压
        public bool Isqy
        {
            get { return this.GetValue(c => c.Isqy); }
            set { this.SetValue(c => c.Isqy, value); }
        }

        //欠流
        public bool Isql
        {
            get { return this.GetValue(c => c.Isql); }
            set { this.SetValue(c => c.Isql, value); }
        }

        //过压
        public bool Isgy
        {
            get { return this.GetValue(c => c.Isgy); }
            set { this.SetValue(c => c.Isgy, value); }
        }

        //过流
        public bool Isgl
        {
            get { return this.GetValue(c => c.Isgl); }
            set { this.SetValue(c => c.Isgl, value); }
        }

        //无输出
        public bool Iswsc
        {
            get { return this.GetValue(c => c.Iswsc); }
            set { this.SetValue(c => c.Iswsc, value); }
        }

        //合格
        public bool Ishg
        {
            get { return this.GetValue(c => c.Ishg); }
            set { this.SetValue(c => c.Ishg, value); }
        }

        //负载保护
        public bool Isfzbh
        {
            get { return this.GetValue(c => c.Isfzbh); }
            set { this.SetValue(c => c.Isfzbh, value); }
        }

        public ICommand SelectCPXHCommand { get { return new DelegateCommand(SelectCPXH); } }

        private LHSJEnum lhsjEnum;
        public ICommand LHSJCommand { get { return new DelegateCommand(LHSJ); } }

        //上电检测
        public ICommand SDJCCommand { get { return new DelegateCommand(SDJC); } }
        public ICommand KSCSCommand { get { return new DelegateCommand(KSCS); } }
        public ICommand ZTCSCommand { get { return new DelegateCommand(ZTCS); } }
        public ICommand TZCSCommand { get { return new DelegateCommand(TZCS); } }

        //查看数据
        public ICommand CKSJCommand { get { return new DelegateCommand(CKSJ); } }
        public ICommand CKTXMCommand { get { return new DelegateCommand(CKTXM); } }
        public ICommand BJFWCommand { get { return new DelegateCommand(BJFW); } }

        private void SelectCPXH()
        {
            var path = FileDialogHelper.OpenFileDialog();
            TestDataCollection.First().cpxh = path;
        }

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

        private bool IsRuning = false;
        private ManualResetEvent mre = new ManualResetEvent(true);
        /// <summary>
        /// 上电检测
        /// </summary>
        private void SDJC()
        {
            if (IsRuning)
                return;

            IsRuning = true;
            Task.Factory.StartNew(() =>
            {
                while (IsRuning)
                {
                    CompareData();
                    int sleep = config.DataSaveSpan.ToInt32() * 1000;
                    sleep = 5000;
                    Thread.Sleep(sleep);
                }
            });
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        private void KSCS()
        {
            //if (IsRuning)
            //    return;
            Task.Factory.StartNew(() =>
            {
                while (IsRuning && mre.WaitOne())
                {
                    Lhzsj++;
                    Lhsj++;
                    Thread.Sleep(1000);
                }
            });
        }

        bool a = true;
        /// <summary>
        /// 暂停测试
        /// </summary>
        private void ZTCS()
        {
            if (a)
            {
                mre.Reset();
            }
            else
            {
                mre.Set();
            }
            a = !a;
        }

        /// <summary>
        /// 停止测试
        /// </summary>
        private void TZCS()
        {
            IsRuning = false;
        }

        /// <summary>
        /// 查看数据
        /// </summary>
        private void CKSJ()
        {
            LiveDataWindow.OpenLiveData();
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

        private void CompareData()
        {
            Istj = !Istj;
            Isxzczz = !Isxzczz;
            Istzlz = !Istzlz;
            Islzyc = !Islzyc;

            Iswcp = !Iswcp;
            Isqy = !Isqy;
            Isql = !Isql;

            Isgy = !Isgy;
            Isgl = !Isgl;

            Iswsc = !Iswsc;
            Ishg = !Ishg;
            Isfzbh = !Isfzbh;
        }
    }
}
