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

        public bool IsAllLayer { get; set; }

        public bool IsAllTC { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MaxCode = numericCount.IncrementText;
            if(MaxCode.ToInt32() > numericCount.MaxWidth)
            {
                MsgBox.WarningShow("超出最大数配置", "");
                return;
            }

            UUTViewModel = new UUTViewModel();
            UUTViewModel.Code = txtCode.Text;

            IsAllLayer = ckbAllLayer.IsChecked.GetValueOrDefault();
            IsAllTC = ckbAllTC.IsChecked.GetValueOrDefault();
            this.DialogResult = true;
        }
    }
}
