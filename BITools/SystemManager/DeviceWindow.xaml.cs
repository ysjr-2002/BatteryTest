using BITools.ViewModel;
using BITools.ViewModel.Configs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// DeviceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceWindow : BaseWindow
    {
        private DeviceConfigViewModel vm = null;
        public DeviceWindow()
        {
            InitializeComponent();
            vm = new DeviceConfigViewModel();
            this.DataContext = vm;
        }

        private void dgChannel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void dgChannelInterface_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgChannelInterface.SelectedItem == null)
            {
                return;
            }

            var channel = (ChannelInterfaceViewModel)dgChannelInterface.SelectedItem;
            var window = new ChannelConfigWindow(channel);
            window.ShowDialog();
        }
    }
}
