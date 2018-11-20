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
        private DeveInfo deveInfo = null;
        private Config sysConfig;
        private Configs.LayerViewModel layerConfig;
        private string dataFolder;

        public LayerViewModel(Configs.LayerViewModel layerConfig, Config config)
        {
            this.layerConfig = layerConfig;
            this.sysConfig = config;
            this.Name = layerConfig.Name;
            this.RunDataCollection = new ObservableCollection<DeveInfo>();
            deveInfo = new DeveInfo
            {
                sbbh = layerConfig.Name,
                sm = 1,
                lhcsdy = "ss",
                acsr = "380V",
                aczt = "OFF",
                zs = 0,
                hg = 0,
                bl = 1,
                bll = "99%"
            };
            this.RunDataCollection.Add(deveInfo);
            this.lhsjEnum = LHSJEnum.LHZSJ;
            this.LHSJName = FunExt.GetDescription(lhsjEnum);

            this.IsSDJCEnable = true;
            this.IsKSCSEnable = true;
            this.IsZTCSEnable = false;
            this.IsTZCSEnable = false;

            this.Lhzsj = 1;
            this.Lhbfb = "等待测试 0%";
        }

        public ObservableCollection<UUTViewModel> UUTList
        {
            get { return this.GetValue(c => c.UUTList); }
            set { this.SetValue(c => c.UUTList, value); }
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

        public ObservableCollection<DeveInfo> RunDataCollection
        {
            get { return this.GetValue(c => c.RunDataCollection); }
            set { this.SetValue(c => c.RunDataCollection, value); }
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

        /// <summary>
        /// 老化百分比
        /// </summary>
        public string Lhbfb
        {
            get { return this.GetValue(c => c.Lhbfb); }
            set { this.SetValue(c => c.Lhbfb, value); }
        }

        public int LhbfbValue
        {
            get { return this.GetValue(c => c.LhbfbValue); }
            set { this.SetValue(c => c.LhbfbValue, value); }
        }

        public void Refresh()
        {
            this.RunDataCollection.First().zs = UUTList.Count;
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

        public bool IsSDJCEnable
        {
            get { return this.GetValue(c => c.IsSDJCEnable); }
            set { this.SetValue(c => c.IsSDJCEnable, value); }
        }

        public bool IsKSCSEnable
        {
            get { return this.GetValue(c => c.IsKSCSEnable); }
            set { this.SetValue(c => c.IsKSCSEnable, value); }
        }

        public bool IsZTCSEnable
        {
            get { return this.GetValue(c => c.IsZTCSEnable); }
            set { this.SetValue(c => c.IsZTCSEnable, value); }
        }

        public bool IsTZCSEnable
        {
            get { return this.GetValue(c => c.IsTZCSEnable); }
            set { this.SetValue(c => c.IsTZCSEnable, value); }
        }

        private void SelectCPXH()
        {
            var path = FileManager.OpenParamFile();
            RunDataCollection.First().cpxh = path;
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

            IsSDJCEnable = false;
            IsRuning = true;
            Task.Factory.StartNew(() =>
            {
                while (IsRuning)
                {
                    ReadData();
                    int sleep = sysConfig.DataSaveSpan.ToInt32() * 1000;
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
            if (deveInfo.cpxh.IsEmpty())
            {
                MsgBox.WarningShow("请选择产品型号！");
                return;
            }

            BuildDataPath();

            IsKSCSEnable = false;
            IsZTCSEnable = true;
            IsTZCSEnable = true;
            Task.Factory.StartNew(() =>
            {
                //老化时间配置的小时
                this.Lhzsj = ((int)this.layerConfig.LHSJ.ToFloat()) * 60 * 60;
                this.RunDataCollection.First().aczt = "On";
                while (IsRuning && mre.WaitOne())
                {
                    Lhsj++;
                    LhbfbValue = (int)(((float)Lhsj / Lhzsj) * 100);
                    Lhbfb = "老化中 " + LhbfbValue + "%";
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
                IsKSCSEnable = true;
                IsZTCSEnable = false;
                Lhbfb = "暂停 " + LhbfbValue + "%";
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

        void BuildDataPath()
        {
            var a = GetFolderName(sysConfig.RootLevel3);
            var b = GetFolderName(sysConfig.RootLevel4);
            var c = GetFolderName(sysConfig.RootLevel5);
            dataFolder = System.IO.Path.Combine(sysConfig.RootLevel1, sysConfig.RootLevel2, a, b, c);

            if (System.IO.Directory.Exists(dataFolder) == false)
                System.IO.Directory.CreateDirectory(dataFolder);
        }

        string GetFolderName(string name)
        {
            if (name == "日期")
                return DateTime.Now.ToString("yyyy-MM-dd");
            else if (name == "机型名")
                return deveInfo.cpxh;
            else //区域
                return "L" + deveInfo.sbbh;
        }
    }
}
