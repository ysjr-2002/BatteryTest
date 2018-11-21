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

namespace BITools.DataManager
{
    /// <summary>
    /// CollectionDataWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CollectionDataWindow : BaseWindow
    {
        private string layer;
        private UUTViewModel uut;
        public CollectionDataWindow(string layer, UUTViewModel uut)
        {
            InitializeComponent();
            this.layer = layer;
            this.uut = uut;
            this.Loaded += CollectionDataWindow_Loaded;
        }

        private void CollectionDataWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbLocation.Text = layer + "-" + uut.Code;
        }
    }
}
