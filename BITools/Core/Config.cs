using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.Core
{
    public class Config : PropertyNotifyObject
    {
        public string ACom
        {
            get { return this.GetValue(c => c.ACom); }
            set { this.SetValue(c => c.ACom, value); }
        }
        public string BCom
        {
            get { return this.GetValue(c => c.BCom); }
            set { this.SetValue(c => c.BCom, value); }
        }
        public string CCom
        {
            get { return this.GetValue(c => c.CCom); }
            set { this.SetValue(c => c.CCom, value); }
        }
        public string DCom
        {
            get { return this.GetValue(c => c.DCom); }
            set { this.SetValue(c => c.DCom, value); }
        }
        public string ECom
        {
            get { return this.GetValue(c => c.ECom); }
            set { this.SetValue(c => c.ECom, value); }
        }
        public string FCom
        {
            get { return this.GetValue(c => c.FCom); }
            set { this.SetValue(c => c.FCom, value); }
        }

        public string AName
        {
            get { return this.GetValue(c => c.AName); }
            set { this.SetValue(c => c.AName, value); }
        }
        public string BName
        {
            get { return this.GetValue(c => c.BName); }
            set { this.SetValue(c => c.BName, value); }
        }
        public string CName
        {
            get { return this.GetValue(c => c.CName); }
            set { this.SetValue(c => c.CName, value); }
        }
        public string DName
        {
            get { return this.GetValue(c => c.DName); }
            set { this.SetValue(c => c.DName, value); }
        }
        public string EName
        {
            get { return this.GetValue(c => c.EName); }
            set { this.SetValue(c => c.EName, value); }
        }
        public string FName
        {
            get { return this.GetValue(c => c.FName); }
            set { this.SetValue(c => c.FName, value); }
        }

        public bool IsAlarm
        {
            get { return this.GetValue(c => c.IsAlarm); }
            set { this.SetValue(c => c.IsAlarm, value); }
        }
        public string S1
        {
            get { return this.GetValue(c => c.S1); }
            set { this.SetValue(c => c.S1, value); }
        }
        public string S2
        {
            get { return this.GetValue(c => c.S2); }
            set { this.SetValue(c => c.S2, value); }
        }
        public bool IsBLJS
        {
            get { return this.GetValue(c => c.IsBLJS); }
            set { this.SetValue(c => c.IsBLJS, value); }
        }
        public string S3
        {
            get { return this.GetValue(c => c.S3); }
            set { this.SetValue(c => c.S3, value); }
        }
        public bool IsBLPZT
        {
            get { return this.GetValue(c => c.IsBLPZT); }
            set { this.SetValue(c => c.IsBLPZT, value); }
        }
        public bool IsSJWCTS
        {
            get { return this.GetValue(c => c.IsSJWCTS); }
            set { this.SetValue(c => c.IsSJWCTS, value); }
        }

        public bool IsSDJCYS
        {
            get { return this.GetValue(c => c.IsSDJCYS); }
            set { this.SetValue(c => c.IsSDJCYS, value); }
        }
        public string S4
        {
            get { return this.GetValue(c => c.S4); }
            set { this.SetValue(c => c.S4, value); }
        }

        public void Read()
        {
            ACom = GetKey("ACom");
            BCom = GetKey("BCom");
            CCom = GetKey("CCom");
            DCom = GetKey("DCom");
            ECom = GetKey("ECom");
            FCom = GetKey("FCom");

            AName = GetKey("AName");
            BName = GetKey("BName");
            CName = GetKey("CName");
            DName = GetKey("DName");
            EName = GetKey("EName");
            FName = GetKey("FName");

            IsAlarm = GetKey("IsAlarm").ToInt32() == 1;
            S1 = GetKey("S1");
            S2 = GetKey("S2");
            IsBLJS = GetKey("IsBLJS").ToInt32() == 1;
            S3 = GetKey("S3");
            IsBLPZT = GetKey("IsBLPZT").ToInt32() == 1;
            IsSJWCTS = GetKey("IsSJWCTS").ToInt32() == 1;

            IsSDJCYS = GetKey("IsSDJCYS").ToInt32() == 1;
            S4 = GetKey("S4");
        }

        public void Save()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfg.AppSettings.Settings["ACom"].Value = ACom;
            cfg.AppSettings.Settings["BCom"].Value = BCom;
            cfg.AppSettings.Settings["CCom"].Value = CCom;
            cfg.AppSettings.Settings["DCom"].Value = DCom;
            cfg.AppSettings.Settings["ECom"].Value = ECom;
            cfg.AppSettings.Settings["FCom"].Value = FCom;

            cfg.AppSettings.Settings["AName"].Value = AName;
            cfg.AppSettings.Settings["BName"].Value = BName;
            cfg.AppSettings.Settings["CName"].Value = CName;
            cfg.AppSettings.Settings["DName"].Value = DName;
            cfg.AppSettings.Settings["EName"].Value = EName;
            cfg.AppSettings.Settings["FName"].Value = FName;

            cfg.AppSettings.Settings["IsAlarm"].Value = IsAlarm.ToString();
            cfg.AppSettings.Settings["S1"].Value = S1;
            cfg.AppSettings.Settings["S2"].Value = S2;
            cfg.AppSettings.Settings["IsBLJS"].Value = IsBLJS.ToString();
            cfg.AppSettings.Settings["S3"].Value = S3;
            cfg.AppSettings.Settings["IsBLPZT"].Value = IsBLPZT.ToString();
            cfg.AppSettings.Settings["IsSJWCTS"].Value = IsSJWCTS.ToString();

            cfg.AppSettings.Settings["IsSDJCYS"].Value = IsSDJCYS.ToString();
            cfg.AppSettings.Settings["S4"].Value = S4;

            cfg.Save(ConfigurationSaveMode.Modified);
        }

        private string GetKey(string key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                var val = ConfigurationManager.AppSettings[key];
                return val;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
