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
    /// LayerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LayerConfigWindow : BaseWindow
    {
        public LayerConfigWindow()
        {
            InitializeComponent();
        }

        public LayerViewModel LayerViewModel { get; set; }

        public string MaxLayer { get; set; }

        public bool IsAllTC { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MaxLayer = numericFloors.IncrementText;
            if (MaxLayer.ToInt32() > numericFloors.MaxValue)
            {
                MsgBox.WarningShow("超出最大层配置", "");
                return;
            }

            LayerViewModel = new LayerViewModel();
            LayerViewModel.Code = txtCode.Text;
            LayerViewModel.Name = txtName.Text;

            IsAllTC = ckbAll.IsChecked.GetValueOrDefault();
            this.DialogResult = true;
        }
    }
}
