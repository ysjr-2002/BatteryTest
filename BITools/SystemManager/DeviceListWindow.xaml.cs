using BIDataAccess.entities;
using BITools.ViewModel.Configs;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace BITools.SystemManager
{
    /// <summary>
    /// DeviceListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceListWindow : BaseWindow
    {
        public DeviceListWindow()
        {
            InitializeComponent();
            this.DataContext = new DeviceListViewModel();
        }

        public DeviceConfig DeviceConfig { get; set; }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (dgList.SelectedItem == null)
            {
                return;
            }
            var modle = (DeviceConfig)dgList.SelectedItem;
            this.DeviceConfig = modle;
            this.DialogResult = true;
        }
    }
}
