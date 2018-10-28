using BITools.ViewModel;
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
        DeviceConfigViewModel vm = null;
        public DeviceWindow()
        {
            InitializeComponent();
            vm = new DeviceConfigViewModel();
            this.DataContext = vm;
            this.Loaded += DeviceConfigWindow_Loaded;
            this.Closing += DeviceConfigWindow_Closing;
        }

        private void DeviceConfigWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var filename = "temp.json";
            if (System.IO.File.Exists(filename))
            {
                var content = System.IO.File.ReadAllText(filename);
                var list = JsonConvert.DeserializeObject<ObservableCollection<ViewModel.Configs.TCViewModel>>(content);
                vm.TCList = list;
            }
        }

        private void DeviceConfigWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var content = JsonConvert.SerializeObject(vm.TCList);
            content = ConvertJsonString(content);
            System.IO.File.WriteAllText("temp.json", content);
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
    }
}
