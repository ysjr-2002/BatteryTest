using BICommon;
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
    /// ConfigNameWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigNameWindow : BaseWindow
    {
        public ConfigNameWindow()
        {
            InitializeComponent();
        }

        public string ConfigName { get; set; }
        public bool IsDefault { get; set; }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.IsEmpty())
            {
                MsgBox.WarningShow("请输入名称！");
                return;
            }
            this.ConfigName = txtName.Text;
            this.IsDefault = ckbDefault.IsChecked.GetValueOrDefault();
            this.DialogResult = true;
        }
    }
}
