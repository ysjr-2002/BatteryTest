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
    /// TCWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TCConfigWindow : BaseWindow
    {
        public TCConfigWindow()
        {
            InitializeComponent();
        }

        public TCViewModel TCViewModel { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TCViewModel = new TCViewModel();
            TCViewModel.Code = txtCode.Text;
            TCViewModel.Name = txtName.Text;
            TCViewModel.Com = txtCom.Text;
            this.DialogResult = true;
        }
    }
}
