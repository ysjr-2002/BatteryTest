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
            AddItem("cao", "整体设置", new ChannelDataGridView());
        }

        void AddItem(string name, string header, ContentControl cc)
        {
            TabItem item = new TabItem { Name = name, Header = header, IsSelected = true };
            item.Style = Application.Current.Resources["TabItem.BigData.Sub.Style"] as System.Windows.Style;

            item.Content = cc;
            RightContainer.Items.Add(item);
        }

        int layerIndex = 0;
        private void btnAddLayer_Click(object sender, RoutedEventArgs e)
        {
            layerIndex++;
            AddItem("layer" + layerIndex, string.Format("第{0:d2}层", layerIndex), new ChannelDataGridView());
        }

        private void btnDeleteLastLayer_Click(object sender, RoutedEventArgs e)
        {
            var tabItem = RightContainer.Items.First(s => s.Name == "layer" + layerIndex);
            RightContainer.Remove(tabItem);
            layerIndex--;
        }

        private void btnCopyLayer_Click(object sender, RoutedEventArgs e)
        {
            //var tab = RightContainer.SelectedItem;
            //layerIndex++;
            //AddItem("layer" + layerIndex, string.Format("第{0:d2}层", layerIndex), (ContentControl)tab.Content);
        }

        private void btnPasteLayer_Click(object sender, RoutedEventArgs e)
        {
            var tab = RightContainer.SelectedItem;
            layerIndex++;
            AddItem("layer" + layerIndex, string.Format("第{0:d2}层", layerIndex), (ContentControl)tab.Content);
        }
    }
}