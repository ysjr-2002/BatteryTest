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
    /// LayerParamWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LayerParamWindow : BaseWindow
    {
        public LayerParamWindow()
        {
            InitializeComponent();
        }

        public LayerViewModel LayerViewModel { get; set; }

        public string MaxLayer { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            LayerViewModel = new LayerViewModel();
            LayerViewModel.Code = txtCode.Text;
            LayerViewModel.Name = txtName.Text;
            //LayerViewModel.LHSJ = txtTime.Text;
            MaxLayer = txtMaxLayer.Text;
            this.DialogResult = true;
        }
    }
}
