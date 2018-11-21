using BILogic;
using BITools.UIControls;
using BITools.ViewModel;
using LL.SenicSpot.Gate.Kernal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BITools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            this.vm = NinjectKernal.Instance.Get<MainViewModel>();
            this.DataContext = vm;
            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
            this.vm.TabControl = tabs;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dialog = MsgBox.QuestionShow("确认退出软件吗?");
            if (dialog == MsgBoxResult.OK)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
