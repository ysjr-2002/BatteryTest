using BITools.DataManager;
using BITools.SystemManager;
using LL.SenicSpot.Gate.Kernal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            NinjectKernal.Instance.Load();
            var window = new MainWindow();
            Application.Current.MainWindow = window;
            window.Show();
        }
    }
}
