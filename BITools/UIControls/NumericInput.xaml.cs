using BICommon;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BITools.UIControls
{
    /// <summary>
    /// NumericInput.xaml 的交互逻辑
    /// </summary>
    public partial class NumericInput : UserControl
    {
        public NumericInput()
        {
            InitializeComponent();
        }

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }

        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(int), typeof(NumericInput), new FrameworkPropertyMetadata(MaxLengthChanged));

        private static void MaxLengthChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var source = sender as FrameworkElement;
                TextBox tb = (TextBox)LogicalTreeHelper.FindLogicalNode(source, "txt");
                tb.MaxLength = (int)e.NewValue;
            }
            catch { }
        }

        public string IncrementText
        {
            get { return (string)GetValue(IncrementTextProperty); }
            set { SetValue(IncrementTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncrementTextProperty =
            DependencyProperty.Register("IncrementText", typeof(string), typeof(NumericInput), new FrameworkPropertyMetadata(TextChanged));

        private static void TextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                var source = sender as NumericInput;
                TextBox tb = (TextBox)LogicalTreeHelper.FindLogicalNode(source, "txt");
                tb.Text = e.NewValue as string;
                //if (source.FloatFormat.IsEmpty() == false)
                //{
                //    if (tb.Text.IsEmpty())
                //        tb.Text = (0f).ToString(source.FloatFormat);
                //    else
                //        tb.Text = float.Parse(tb.Text).ToString(source.FloatFormat);
                //}
            }
            catch { }
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.RegisterAttached("MaxValue", typeof(int), typeof(NumericInput), new
FrameworkPropertyMetadata(MaxValueChanged));

        private static void MaxValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //SelectNumeric source = (SelectNumeric)sender;
            //source.txt_TextChanged(null, null);
        }

        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.RegisterAttached("MinValue", typeof(int), typeof(NumericInput), new
FrameworkPropertyMetadata(MinValueChanged));

        private static void MinValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            //SelectNumeric source = (SelectNumeric)sender;
            //source.txt_TextChanged(null, null);
        }

        public string FloatFormat
        {
            get { return (string)GetValue(FloatFormatProperty); }
            set { SetValue(FloatFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FloatFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FloatFormatProperty =
            DependencyProperty.Register("FloatFormat", typeof(string), typeof(NumericInput), new FrameworkPropertyMetadata(FormatChanged));

        private static void FormatChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var val = e.NewValue.ToString();
            var source = sender as FrameworkElement;
            TextBox tb = (TextBox)LogicalTreeHelper.FindLogicalNode(source, "txt");
            if (tb.Text.IsEmpty())
                tb.Text = (0f).ToString(val);
            else
                tb.Text = float.Parse(tb.Text).ToString(val);
        }

        private void txt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var b = (byte)e.Key;
            if (b != 88 && b != 2 && !((b >= 34 && b <= 43)) && !(b >= 74 && b <= 83) && b != 32 && b != 23 && b != 25)
            {
                e.Handled = true;
            }
        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            IncrementText = txt.Text;
        }
    }
}
