using BICommon.Enums;
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
    /// ChannelConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChannelConfigWindow : BaseWindow
    {
        private ChannelInterfaceViewModel channelInterface;

        public ChannelConfigWindow(ChannelInterfaceViewModel channelInterface)
        {
            InitializeComponent();
            this.channelInterface = channelInterface;
            cmbInterface.SelectionChanged += CmbInterface_SelectionChanged;
            CmbInterface_SelectionChanged(null, null);
            cmbCom.ItemsSource = FunExt.GetSerialPorts();
            this.Loaded += ChannelConfigWindow_Loaded;
        }

        private void ChannelConfigWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (channelInterface != null)
            {
                txtCode.Text = channelInterface.Code;
                cmbType.SelectedIndex = channelInterface.OutputType;
                cmbInterface.SelectedIndex = channelInterface.InterfaceType;

                if ((InterfaceEnum)channelInterface.InterfaceType == InterfaceEnum.AAA)
                    txtNo.IncrementText = channelInterface.Address;
                if ((InterfaceEnum)channelInterface.InterfaceType == InterfaceEnum.Com)
                    cmbCom.Text = channelInterface.Address;
            }
        }

        private void CmbInterface_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInterface.SelectedIndex == 0)
            {
                cmbCom.Visibility = Visibility.Collapsed;
                txtNo.Visibility = Visibility.Visible;
            }
            else if (cmbInterface.SelectedIndex == 1)
            {
                cmbCom.Visibility = Visibility.Visible;
                txtNo.Visibility = Visibility.Collapsed;
            }
        }

        public ViewModel.Configs.ChannelInterfaceViewModel ChannelInterfaceViewModel { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (channelInterface == null)
                ChannelInterfaceViewModel = new ChannelInterfaceViewModel();
            else
                ChannelInterfaceViewModel = channelInterface;

            ChannelInterfaceViewModel.Code = txtCode.Text;
            ChannelInterfaceViewModel.OutputType = cmbType.SelectedIndex;
            ChannelInterfaceViewModel.InterfaceType = cmbInterface.SelectedIndex;
            if (cmbInterface.SelectedIndex == 0)
                ChannelInterfaceViewModel.Address = txtNo.IncrementText;
            if (cmbInterface.SelectedIndex == 1)
                ChannelInterfaceViewModel.Address = cmbCom.Text;
            this.DialogResult = true;
        }
    }
}
