using BIModel;
using BITools.DataManager;
using BITools.SystemManager;
using LL.SenicSpot.Gate.Kernal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BITools
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /*
          负载通道1 电压电流上下限
          配置和参数加使、能参数
          点击机台，可以查看机台上次采集数据
        */
        protected override void OnStartup(StartupEventArgs e)
        {
            var bnew = false;
            var appname = System.Windows.Forms.Application.ProductName;
            var mutex = new Mutex(true, appname, out bnew);
            if (bnew)
            {
                AppContext.UserName = "admin";
                NinjectKernal.Instance.Load();
                var window = new DeviceRunTimeSettingWindow();
                Application.Current.MainWindow = window;
                window.Show();
            }
            else
            {
                MessageBox.Show("系统已运行！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
                Environment.Exit(Environment.ExitCode);
            }
        }
    }
}
