using BITools.Enums;
using BITools.Helpers;
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
    public partial class DeviceRunTimeSettingWindow : BaseWindow
    {
        DeviceRunTimeSettingViewModel vm = null;
        public DeviceRunTimeSettingWindow()
        {
            InitializeComponent();
            vm = new DeviceRunTimeSettingViewModel();
            this.DataContext = vm;
        }

        private void ButtonEx_Click(object sender, RoutedEventArgs e)
        {
            var selectedChannel = dgChannel.SelectedItem as ChannelViewModel;
            if (selectedChannel == null)
                return;

            foreach (object o in dgMonitorParam.Items)
            {
                DataGridRow rowItem = dgMonitorParam.ItemContainerGenerator.ContainerFromItem(o) as DataGridRow;
                var model = rowItem.DataContext as MonitorParamViewModel;
                if (model.ValType == (int)ValTypeEnum.Selector)
                {
                    var cmbCSMS = UIHelper.FindChild<ComboBox>(rowItem, "cmbCSMS");
                    model.Val = cmbCSMS.SelectedIndex.ToString();
                }
                else
                {
                    var txtVal = UIHelper.FindChild<TextBox>(rowItem, "txtVal");
                    model.Val = txtVal.Text;
                }
            }

            var list = dgMonitorParam.ItemsSource as ObservableCollection<MonitorParamViewModel>;
            if (ckbLayer.IsChecked.GetValueOrDefault())
            {
                var tc = dgTC.SelectedItem as TCViewModel;
                foreach (var layer in tc.LayerList)
                {
                    foreach (var uut in layer.UUTList)
                    {
                        foreach (var channel in uut.ChannelList)
                        {
                            if (selectedChannel.Code == channel.Code)
                            {
                                channel.MontiorParamList = selectedChannel.MontiorParamList;
                            }
                        }
                    }
                }
            }

            if (ckbTC.IsChecked.GetValueOrDefault())
            {
                foreach (var tc in this.vm.TCList)
                {
                    foreach (var layer in tc.LayerList)
                    {
                        foreach (var uut in layer.UUTList)
                        {
                            foreach (var channel in uut.ChannelList)
                            {
                                if (selectedChannel.Code == channel.Code)
                                {
                                    channel.MontiorParamList = selectedChannel.MontiorParamList;
                                }
                            }
                        }
                    }
                }
            }
            MsgBox.SuccessShow("保存成功！");
        }
    }
}
