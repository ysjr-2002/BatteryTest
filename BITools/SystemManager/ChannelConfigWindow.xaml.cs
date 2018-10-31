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
        public ChannelConfigWindow()
        {
            InitializeComponent();
        }

        public ViewModel.Configs.ChannelViewModel ChannelViewModel { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ChannelViewModel = new ViewModel.Configs.ChannelViewModel();
            ChannelViewModel.Code = txtCode.Text;
            ChannelViewModel.OutputType = cmbType.SelectedIndex;
            ChannelViewModel.Value = txtZ.Text;
            ChannelViewModel.SX = txtSX.Text;
            ChannelViewModel.XX = txtXX.Text;
            ChannelViewModel.Temperature = txtTemperature.Text;
            this.DialogResult = true;
        }
    }
}
