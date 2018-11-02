using BITools.Enums;
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
        private ChannelViewModel channel;

        public ChannelConfigWindow(ChannelViewModel channel)
        {
            InitializeComponent();
            this.channel = channel;
            cmbInterface.SelectionChanged += CmbInterface_SelectionChanged;
            CmbInterface_SelectionChanged(null, null);
            cmbCom.ItemsSource = FunExt.GetSerialPorts();
            this.Loaded += ChannelConfigWindow_Loaded;
        }

        private void ChannelConfigWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //if (channel != null)
            //{
            //    txtCode.Text = channel.Code;
            //    cmbType.SelectedIndex = channel.OutputType;
            //    cmbInterface.SelectedIndex = channel.InterfaceType;

            //    if ((InterfaceEnum)channel.InterfaceType == InterfaceEnum.AAA)
            //        txtNo.IncrementText = channel.Name;
            //    if ((InterfaceEnum)channel.InterfaceType == InterfaceEnum.Com)
            //        cmbCom.Text = channel.Name;
            //}
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

        public ViewModel.Configs.ChannelViewModel ChannelViewModel { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //ChannelViewModel = new ViewModel.Configs.ChannelViewModel();
            //ChannelViewModel.Code = txtCode.Text;
            //ChannelViewModel.OutputType = cmbType.SelectedIndex;
            //ChannelViewModel.InterfaceType = cmbInterface.SelectedIndex;
            //if (cmbInterface.SelectedIndex == 0)
            //    ChannelViewModel.Name = txtNo.IncrementText;
            //if (cmbInterface.SelectedIndex == 1)
            //    ChannelViewModel.Name = cmbCom.Text;
            //this.DialogResult = true;
        }
    }
}
