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
            this.Loaded += DeviceRunTimeSettingWindow_Loaded;
            this.Closing += DeviceRunTimeSettingWindow_Closing;
        }

        private void DeviceRunTimeSettingWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //var filename = "temp.json";
            //if (System.IO.File.Exists(filename))
            //{
            //    var content = System.IO.File.ReadAllText(filename);
            //    var list = JsonConvert.DeserializeObject<ObservableCollection<ViewModel.Configs.TCViewModel>>(content);
            //    vm.TCList = list;
            //}
        }

        private void DeviceRunTimeSettingWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //var content = JsonConvert.SerializeObject(vm.TCList);
            //content = FunExt.JsonFormatter(content);
            //System.IO.File.WriteAllText("temp.json", content);
        }

        private void ButtonEx_Click(object sender, RoutedEventArgs e)
        {
            var list = dgMonitorParam.ItemsSource as ObservableCollection<MonitorParamViewModel>;
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
        }
    }
}
