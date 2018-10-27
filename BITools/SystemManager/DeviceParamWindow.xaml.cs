using BITools.UIControls;
using BITools.ViewModel;
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
    /// DeviceParamWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceParamWindow : BaseWindow
    {
        public DeviceParamWindow()
        {
            InitializeComponent();
            this.DataContext = new DeviceParamViewModel();
            this.Loaded += DeviceParamWindow_Loaded;
        }

        private void DeviceParamWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AddItem("cao", "整体设置");
        }

        void AddItem(string name, string header)
        {
            TabItem item = new TabItem { Name = name, Header = header, IsSelected = true };
            item.Style = Application.Current.Resources["TabItem.BigData.Sub.Style"] as System.Windows.Style;

            item.Content = new ChannelDataGridView(); 
            RightContainer.Items.Add(item);
        }
    }
}