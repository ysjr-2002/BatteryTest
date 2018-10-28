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
    /// UUTConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UUTConfigWindow : BaseWindow
    {
        public UUTConfigWindow()
        {
            InitializeComponent();
        }

        public UUTViewModel UUTViewModel { get; set; }

        public string MaxCode { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            UUTViewModel = new UUTViewModel();
            UUTViewModel.Code = txtCode.Text;

            MaxCode = txtMaxCode.Text;
            this.DialogResult = true;
        }
    }
}
