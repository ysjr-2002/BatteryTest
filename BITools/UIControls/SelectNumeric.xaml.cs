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
    public partial class SelectNumeric : UserControl
    {
        bool isError = false;
        Brush lastBdBrush = Application.Current.Resources["TextBox.Border.Brush"] as Brush;
        public SelectNumeric()
        {
            InitializeComponent();
            bd.BorderBrush = lastBdBrush;
            Increment = 0;
        }

        public int Increment
        {
            get { return (int)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }
        public static readonly DependencyProperty IncrementProperty = DependencyProperty.RegisterAttached("Increment", typeof(int), typeof(SelectNumeric), new
FrameworkPropertyMetadata(IncrementChanged));

        private static void IncrementChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                int newValue = (int)e.NewValue;
                SelectNumeric item = sender as SelectNumeric;
                TextBox tb = (TextBox)LogicalTreeHelper.FindLogicalNode(item, "txt");
                //TextBox tb = UIHelper.FindChild<TextBox>(item, "txt");
                tb.Text = newValue.ToString();
            }
            catch { }
        }

        public event EventHandler<EventArgs> IncrementTextChangedEvent;

        public string IncrementText
        {
            get { return (string)GetValue(IncrementTextProperty); }
            set
            {
                SetValue(IncrementTextProperty, value);
                if (IncrementTextChangedEvent != null)
                    IncrementTextChangedEvent(this, new EventArgs());
            }
        }
        public static readonly DependencyProperty IncrementTextProperty = DependencyProperty.RegisterAttached("IncrementText", typeof(string), typeof(SelectNumeric), new
FrameworkPropertyMetadata(IncrementTextChanged));
        private static void IncrementTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (e.NewValue == null) return;
                SelectNumeric item = sender as SelectNumeric;
                item.Increment = Convert.ToInt32(e.NewValue);
                item.txt.Text = item.Increment.ToString();
            }
            catch { }
        }

        public int MaxValue
        {
            get { return (int)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.RegisterAttached("MaxValue", typeof(int), typeof(SelectNumeric), new
FrameworkPropertyMetadata(MaxValueChanged));

        private static void MaxValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SelectNumeric source = (SelectNumeric)sender;
            source.txt_TextChanged(null, null);
        }


        public int MinValue
        {
            get { return (int)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.RegisterAttached("MinValue", typeof(int), typeof(SelectNumeric), new
FrameworkPropertyMetadata(MinValueChanged));

        private static void MinValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SelectNumeric source = (SelectNumeric)sender;
            source.txt_TextChanged(null, null);
        }

        public static readonly DependencyProperty WaterMarkProperty =
           DependencyProperty.Register("WaterMark", typeof(string),
           typeof(SelectNumeric),
           new PropertyMetadata("TextBlock", new PropertyChangedCallback(OnWaterMarkChanged)));
        public string WaterMark
        {
            get { return (string)GetValue(WaterMarkProperty); }

            set { SetValue(WaterMarkProperty, value); }
        }

        static void OnWaterMarkChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var newValue = (string)args.NewValue;
            SelectNumeric source = (SelectNumeric)sender;
            source.txtWaterMark.Text = newValue;
            if (!string.IsNullOrEmpty(newValue))
            {
                source.txtWaterMark.Visibility = Visibility.Visible;
            }
            else
            {
                source.txtWaterMark.Visibility = Visibility.Collapsed;
            }
        }


        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (Increment < MaxValue)
                Increment++;
            EnableBtn();

        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (Increment > MinValue)
                Increment--;
            EnableBtn();
        }

        void EnableBtn()
        {
            try
            {
                UpButton.IsEnabled = true;
                DownButton.IsEnabled = true;
                if (Increment >= MaxValue) UpButton.IsEnabled = false;
                if (Increment <= MinValue) DownButton.IsEnabled = false;
            }
            catch { }
        }

        private void txt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            byte b = (byte)e.Key;
            if (!((b >= 34 && b <= 43)) && !(b >= 74 && b <= 83) && b != 2 && b != 32 && b != 23 && b != 25)
            {
                e.Handled = true;
            }
        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            IncrementText = txt.Text;
            int NewValue = 0;
            var rst = Int32.TryParse(txt.Text, out NewValue);
            if (rst && NewValue >= MinValue && NewValue <= MaxValue)
            {
                Increment = NewValue;
                EnableBtn();
                bd.BorderBrush = lastBdBrush;
                isError = false;
            }
            else
            {
                bd.BorderBrush = Application.Current.Resources["TextBox.Alarm.Border.Brush"] as Brush;
                isError = true;
            }
        }

        private void bd_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isError)
            {
                lastBdBrush = bd.BorderBrush = Application.Current.Resources["TextBox.Border.MouseOver.Brush"] as Brush;
            }
        }

        private void bd_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isError)
            {
                lastBdBrush = bd.BorderBrush = Application.Current.Resources["TextBox.Border.Brush"] as Brush;
            }
        }

        private void txt_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
            if (string.IsNullOrEmpty(this.txt.SelectedText)) return;
            if (e.Delta < 0)
            {
                DownButton_Click(null, null);
            }
            else
            {

                UpButton_Click(null, null);
            }
            this.txt.SelectAll();
        }

        private void SelectAddress(object sender, RoutedEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void SelectivelyIgnoreMouseButton(object sender,
            MouseButtonEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            if (tb != null)
            {
                if (!tb.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    tb.Focus();
                }
            }
        }

    }
}
