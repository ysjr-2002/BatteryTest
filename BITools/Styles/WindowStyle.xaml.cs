using BITools.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BITools.Styles
{
    /// <summary>
    /// WindowStyle.xaml 的交互逻辑
    /// </summary>
    public partial class WindowStyle : ResourceDictionary
    {
        public WindowStyle()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.Close();
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            window.WindowState = WindowState.Minimized;
        }


        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var window = (Window)((FrameworkElement)sender).TemplatedParent;
                window.DragMove();
            }
            catch { }
        }

        private void CloseBTN_Clicked(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(((FrameworkElement)e.Source)).Close();
        }

        private void SizeChangedHandler(object sender, EventArgs e)
        {
            var window = (Window)sender;
            var btn = UIHelper.FindChild<System.Windows.Controls.Primitives.ToggleButton>(window, "Max_MinTogBtn");
            if (window.WindowState == WindowState.Normal)
            {
                btn.IsChecked = false;
            }
            else if (window.WindowState == WindowState.Maximized)
            {
                btn.IsChecked = true;
            }
        }

        private void ToggleButton_Loaded(object sender, RoutedEventArgs e)
        {
            var window = (Window)((FrameworkElement)sender).TemplatedParent;
            if (window.WindowState == WindowState.Maximized)
            {
                (sender as ToggleButton).IsChecked = true;
            }
            else if (window.WindowState == WindowState.Maximized)
            {
                (sender as ToggleButton).IsChecked = false;
            }
        }
    }
}
