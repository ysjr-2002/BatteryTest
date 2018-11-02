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
            this.Loaded += DeviceConfigWindow_Loaded;
            this.Closing += DeviceConfigWindow_Closing;
        }

        private void DeviceConfigWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void DeviceConfigWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //var content = JsonConvert.SerializeObject(vm.TCList);
            //content = ConvertJsonString(content);
            //File.WriteAllText("temp.json", content);
        }

        private string ConvertJsonString(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        private void ButtonEx_Click(object sender, RoutedEventArgs e)
        {
            var list = dgMonitorParam.ItemsSource as ObservableCollection<MonitorParamViewModel>;

            foreach (object o in dgMonitorParam.Items)
            {
                DataGridRow lbi = dgMonitorParam.ItemContainerGenerator.ContainerFromItem(o) as DataGridRow;
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(lbi);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                var aa = myDataTemplate.FindName("abc", myContentPresenter) ;
                //tbhaha.IsChecked = false;
            }
            //tb.IsChecked = true;
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

    }
}
