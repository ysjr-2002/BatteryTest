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
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = NinjectKernal.Instance.Get<MainViewModel>();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var filename = "temp.json";
            if (System.IO.File.Exists(filename))
            {
                var content = System.IO.File.ReadAllText(filename);
                var tcList = JsonConvert.DeserializeObject<ObservableCollection<ViewModel.Configs.TCViewModel>>(content);

                string first = "";
                foreach (var tc in tcList)
                {
                    //台车
                    if (first.IsEmpty())
                        first = tc.Name;

                    TabItem item = new TabItem { Name = tc.Name, Header = tc.Name, IsSelected = true };
                    item.Style = Application.Current.Resources["TabItem.TC"] as System.Windows.Style;

                    var list = new ListBox();
                    list.ItemContainerStyle = this.FindResource("kk") as Style;
                    foreach (var layer in tc.LayerList)
                    {
                        //层
                        LayerView temp = new LayerView();
                        LayerViewModel datacontext = new LayerViewModel(layer.Name);
                        datacontext.UUTList = layer.UUTList;
                        temp.DataContext = datacontext;
                        list.Items.Add(temp);
                    }
                    item.Content = list;
                    tabs.Items.Add(item);
                }
                tabs.SetSelectedItem(first);
            }
        }
    }
}
