using BIDataAccess;
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

        11-21
        0.老化参数为调用文件名
        1.AC输入为可输入，增加保存按钮，保存后不可修改
        2.状态对齐，加边框醒目
        3.层次间增加分隔符
        4.开始测试要醒目
        */
        protected override void OnStartup(StartupEventArgs e)
        {
            SqliteHelper.Instance.Init("bi.data");
            DataOperator.Instance.CreateOrderTable();
            DataOperator.Instance.CreateOrderDataTable();

            var bnew = false;
            var appname = System.Windows.Forms.Application.ProductName;
            var mutex = new Mutex(true, appname, out bnew);
            if (bnew)
            {
                AppContext.UserName = "admin";
                NinjectKernal.Instance.Load();
                var window = new MainWindow();
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
