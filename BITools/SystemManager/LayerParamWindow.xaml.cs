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
    /// 层参数编辑
    /// </summary>
    public partial class LayerParamWindow : BaseWindow
    {
        private LayerViewModel layer = null;

        public LayerParamWindow(LayerViewModel layer)
        {
            InitializeComponent();
            this.layer = layer;
            this.Loaded += LayerParamWindow_Loaded;
        }

        private void LayerParamWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtTime.Text = layer.LHSJ;
        }

        public LayerViewModel LayerViewModel { get; set; }

        public bool IsAllLayer { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtTime.Text.ToFloat() <= 0)
            {
                MsgBox.WarningShow("老化时间不能小于零");
                return;
            }
            this.layer.LHSJ = txtTime.Text.ToFloat().ToString("f2");
            this.IsAllLayer = ckbAllLayer.IsChecked.GetValueOrDefault();
            this.LayerViewModel = layer;
            this.DialogResult = true;
        }

        private void txtTime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Decimal || (e.Key > Key.NumPad0 || e.Key < Key.NumPad8) || e.Key == Key.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
