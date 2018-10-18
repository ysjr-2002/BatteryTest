using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.Core
{
    class Config
    {
        private Config()
        {
        }

        public int AutoRun { get; set; }
        public string Cloud_Server { get; set; }
        public string QR_Server { get; set; }
        public string FACE_Server { get; set; }

        private static Config _current = new Config();

        public static Config Current
        {
            get
            {
                return _current;
            }
        }

        public void Read()
        {
            AutoRun = GetKey("auto").ToInt32();
            Cloud_Server = GetKey("cloud_server");
        }

        public void Save()
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            cfg.AppSettings.Settings["auto"].Value = AutoRun.ToString();
            cfg.AppSettings.Settings["cloud_server"].Value = Cloud_Server;
            cfg.Save();
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
