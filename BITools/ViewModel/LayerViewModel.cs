using BIModel;
using BITools.Core;
using BITools.DataManager;
using BITools.Enums;
using BITools.Helpers;
using BITools.ViewModel.Configs;
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
                zs = 0,
                hg = 0,
                bl = 1,
                bll = "99%"
            });
            lhsjEnum = LHSJEnum.LHZSJ;
            LHSJName = FunExt.GetDescription(lhsjEnum);

            IsSDEnable = true;
        }

        public ObservableCollection<UUTViewModel> UUTList
        {
            get { return this.GetValue(c => c.UUTList); }
            set { this.SetValue(c => c.UUTList, value); }
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

        public ObservableCollection<DeveInfo> TestDataCollection
        {
            get { return this.GetValue(c => c.TestDataCollection); }
            set { this.SetValue(c => c.TestDataCollection, value); }
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

        public void Refresh()
        {
            this.TestDataCollection.First().zs = UUTList.Count;
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

        public bool IsSDEnable
        {
            get { return this.GetValue(c => c.IsSDEnable); }
            set { this.SetValue(c => c.IsSDEnable, value); }
        }

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

            IsSDEnable = false;
            IsRuning = true;
            Task.Factory.StartNew(() =>
            {
                while (IsRuning)
                {
                    ReadData();
                    int sleep = config.DataSaveSpan.ToInt32() * 1000;
                    sleep = 500;
                    Thread.Sleep(sleep);
                }
            });
        }

        /// <summary>
        /// 开始测试
        /// </summary>
        private void KSCS()
        {
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

        private void ReadData()
        {
            if (UUTList.Count == 0)
                return;

            var fzcount = UUTList[0].ChannelList.Count(c => c.ChannelType == (int)ChannelTypeEnum.FZ);
            for (int i = 0; i < UUTList.Count; i++)
            {
                var uut = UUTList[i];
                var message = "";
                GetMeasValueByUUTIndex(i, out message);

                uut.ChangeState();

                var items = message.Split(';');
                if (items.Length != fzcount)
                    break;

                //负载电压开始
                var s = 3;
                for (int j = 0; j < items.Length; j++)
                {
                    var fz = message[j];
                    var fzval = uut.ChannelList[j].MontiorParamList[1].Val;
                    var v = uut.ChannelList[j].MontiorParamList[s].Val.ToFloat();
                    var a = uut.ChannelList[j].MontiorParamList[s + 1].Val.ToFloat();
                    s += 2;
                }
                Console.WriteLine(i);

                Thread.Sleep(1);
            }
        }

        private void GetMeasValueByUUTIndex(int uutIndex, out string message)
        {
            var seed = (int)(DateTime.Now.Ticks & 0xFFFFFFFF);
            //负载1 电压,电流
            var fz1 = new Random(seed).Next(10, 15) + "," + new Random(seed).Next(1, 5);
            //负载2 电压,电流
            var fz2 = new Random(seed).Next(10, 15) + "," + new Random(seed).Next(1, 5);

            message = string.Concat(fz1, ";", fz2);
        }
    }
}
