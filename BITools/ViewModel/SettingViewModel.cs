using BIModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using BITools.Core;

namespace BITools.ViewModel
{
    /// <summary>
    /// 系统设置ViewModel
    /// </summary>
    public class SettingViewModel : BaseViewModel
    {
        private Config config;
        public SettingViewModel()
        {
            config = new Config();
            Config = config;
        }

        public ObservableCollection<string> ComPortCollection
        {
            get { return this.GetValue(c => c.ComPortCollection); }
            set { this.SetValue(c => c.ComPortCollection, value); }
        }

        public Config Config
        {
            get { return config; }
            private set
            {
                config = value;
            }
        }

        //public string AComName
        //{
        //    get { return this.GetValue(c => c.AComName); }
        //    set { this.SetValue(c => c.AComName, value); }
        //}

        //public string BComName
        //{
        //    get { return this.GetValue(c => c.BComName); }
        //    set { this.SetValue(c => c.BComName, value); }
        //}

        //public string CComName
        //{
        //    get { return this.GetValue(c => c.CComName); }
        //    set { this.SetValue(c => c.CComName, value); }
        //}

        //public string DComName
        //{
        //    get { return this.GetValue(c => c.DComName); }
        //    set { this.SetValue(c => c.DComName, value); }
        //}

        //public string EComName
        //{
        //    get { return this.GetValue(c => c.EComName); }
        //    set { this.SetValue(c => c.EComName, value); }
        //}

        //public string FComName
        //{
        //    get { return this.GetValue(c => c.FComName); }
        //    set { this.SetValue(c => c.FComName, value); }
        //}

        //#region  tab2
        //public string ACodeName
        //{
        //    get { return this.GetValue(c => c.ACodeName); }
        //    set { this.SetValue(c => c.ACodeName, value); }
        //}

        //public string BCodeName
        //{
        //    get { return this.GetValue(c => c.BCodeName); }
        //    set { this.SetValue(c => c.BCodeName, value); }
        //}

        //public string CCodeName
        //{
        //    get { return this.GetValue(c => c.CCodeName); }
        //    set { this.SetValue(c => c.CCodeName, value); }
        //}

        //public string DCodeName
        //{
        //    get { return this.GetValue(c => c.DCodeName); }
        //    set { this.SetValue(c => c.DCodeName, value); }
        //}

        //public string ECodeName
        //{
        //    get { return this.GetValue(c => c.ECodeName); }
        //    set { this.SetValue(c => c.ECodeName, value); }
        //}

        //public string FCodeName
        //{
        //    get { return this.GetValue(c => c.FCodeName); }
        //    set { this.SetValue(c => c.FCodeName, value); }
        //}
        //#endregion

        //public bool IsAlarm
        //{
        //    get { return this.GetValue(c => c.IsAlarm); }
        //    set { this.SetValue(c => c.IsAlarm, value); }
        //}

        //public string S1
        //{
        //    get { return this.GetValue(c => c.S1); }
        //    set { this.SetValue(c => c.S1, value); }
        //}

        //public string S2
        //{
        //    get { return this.GetValue(c => c.S2); }
        //    set { this.SetValue(c => c.S2, value); }
        //}

        //public bool IsBLJS
        //{
        //    get { return this.GetValue(c => c.IsBLJS); }
        //    set { this.SetValue(c => c.IsBLJS, value); }
        //}

        //public string S3
        //{
        //    get { return this.GetValue(c => c.S3); }
        //    set { this.SetValue(c => c.S3, value); }
        //}

        //public bool IsBLPZT
        //{
        //    get { return this.GetValue(c => c.IsBLPZT); }
        //    set { this.SetValue(c => c.IsBLPZT, value); }
        //}

        //public bool IsSJWCTS
        //{
        //    get { return this.GetValue(c => c.IsSJWCTS); }
        //    set { this.SetValue(c => c.IsSJWCTS, value); }
        //}

        //#region tab5
        //public bool IsSDJCYS
        //{
        //    get { return this.GetValue(c => c.IsSDJCYS); }
        //    set { this.SetValue(c => c.IsSDJCYS, value); }
        //}

        //public string S4
        //{
        //    get { return this.GetValue(c => c.S4); }
        //    set { this.SetValue(c => c.S4, value); }
        //} 
        //#endregion

        protected override void Loaded()
        {
            ComPortCollection = new ObservableCollection<string>(FunExt.GetSerialPorts());

            Config.Read();

            //this.AComName = config.ACom;
            //this.BComName = config.BCom;
            //this.CComName = config.CCom;
            //this.DComName = config.DCom;
            //this.EComName = config.ECom;
            //this.FComName = config.FCom;

            //this.ACodeName = config.AName;
            //this.BCodeName = config.BName;
            //this.CCodeName = config.CName;
            //this.DCodeName = config.DName;
            //this.ECodeName = config.EName;
            //this.FCodeName = config.FName;

            //this.IsAlarm = config.IsAlarm;
            //this.S1 = config.S1;
            //this.S2 = config.S2;
            //this.IsBLJS = config.IsBLJS;
            //this.S3 = config.S3;
            //this.IsBLPZT = config.IsBLPZT;
            //this.IsSJWCTS = config.IsSJWCTS;

            //this.IsSDJCYS = config.IsSDJCYS;
            //this.S4 = config.S4;
        }

        protected override void Save()
        {
            config.Save();
        }
    }
}
