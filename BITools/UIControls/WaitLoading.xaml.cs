using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BITools.UIControls
{
    /// <summary>
    /// HZWaitLoading.xaml 的交互逻辑
    /// </summary>
    public partial class WaitLoading : UserControl
    {
        public WaitLoading()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty =
                   DependencyProperty.Register("Text", typeof(string),
                   typeof(WaitLoading),
                   new PropertyMetadata("TextBox", new PropertyChangedCallback(OnTextChanged)));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }

            set { SetValue(TextProperty, value); }
        }

        static void OnTextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var newValue = (string)args.NewValue;
            if (!string.IsNullOrEmpty(newValue))
            {
                WaitLoading source = (WaitLoading)sender;
                source.txt.Text = (string)args.NewValue;
            }
        }

    }
}
