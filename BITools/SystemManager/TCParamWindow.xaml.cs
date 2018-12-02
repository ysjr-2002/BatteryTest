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
    /// TCParamWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TCParamWindow : BaseWindow
    {
        public TCParamWindow()
        {
            InitializeComponent();
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
