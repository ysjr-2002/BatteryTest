using BICommon;
using BICommon.Enums;
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

            List<string> tempList = new List<string>();
            foreach (object o in dgMonitorParam.Items)
            {
                DataGridRow rowItem = dgMonitorParam.ItemContainerGenerator.ContainerFromItem(o) as DataGridRow;
                var model = rowItem.DataContext as MonitorParamViewModel;
                if (model.InputMode == (int)InputModeEnum.Selector)
                {
                    var cmbCSMS = UIHelper.FindChild<ComboBox>(rowItem, "cmbCSMS");
                    model.Val = cmbCSMS.SelectedIndex.ToString();
                }
                else
                {
                    var txtVal = UIHelper.FindChild<TextBox>(rowItem, "txtVal");
                    var val = txtVal.Text;
                    model.Val = val;
                    tempList.Add(val);
                }
            }

            if (tempList.Any(s => s.ToFloat() == 0))
            {
                MsgBox.WarningShow("电压或电流值不能为零");
                return;
            }
            if ((tempList[2].ToFloat() >= tempList[3].ToFloat()) || (tempList[4].ToFloat() >= tempList[5].ToFloat()))
            {
                MsgBox.WarningShow("上限值不能高于下限值");
                return;
            }

            ((MonitorParamViewModel)dgMonitorParam.Items[1]).Val = tempList[0];
            ((MonitorParamViewModel)dgMonitorParam.Items[2]).Val = tempList[1];
            ((MonitorParamViewModel)dgMonitorParam.Items[3]).Val = tempList[2];
            ((MonitorParamViewModel)dgMonitorParam.Items[4]).Val = tempList[3];
            ((MonitorParamViewModel)dgMonitorParam.Items[5]).Val = tempList[4];
            ((MonitorParamViewModel)dgMonitorParam.Items[6]).Val = tempList[5];

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
                            if (channel.ChannelType == (int)ChannelTypeEnum.FZ)
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
                                if (channel.ChannelType == (int)ChannelTypeEnum.FZ)
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
