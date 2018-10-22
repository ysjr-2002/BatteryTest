using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace BITools.UIControls
{
    public class WatermarkTextBox : TextBox
    {
        static WatermarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkTextBox), new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));
        }

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public Style WatermarkStyle
        {
            get { return (Style)GetValue(WatermarkStyleProperty); }
            set { SetValue(WatermarkStyleProperty, value); }
        }

        public static Style GetWatermarkStyle(DependencyObject obj)
        {
            return (Style)obj.GetValue(WatermarkStyleProperty);
        }

        public VerticalAlignment WatermarkVerticalAlignment
        {
            set
            {
                SetValue(WatermarkVerticalAlignmentProperty, value);
            }
            get
            {
                return (VerticalAlignment)GetValue(WatermarkVerticalAlignmentProperty);
            }
        }
        public static void SetWatermarkStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(WatermarkStyleProperty, value);
        }

        public static readonly DependencyProperty WatermarkStyleProperty =
            DependencyProperty.RegisterAttached("WatermarkStyle", typeof(Style), typeof(WatermarkTextBox));

        public static readonly DependencyProperty WatermarkVerticalAlignmentProperty =
            DependencyProperty.RegisterAttached("WatermarkVerticalAlignment", typeof(VerticalAlignment), typeof(WatermarkTextBox));

        public static string GetWatermark(DependencyObject obj)
        {
            return (string)obj.GetValue(WatermarkProperty);
        }

        public static void SetWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(WatermarkProperty, value);
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached("Watermark", typeof(string), typeof(WatermarkTextBox),
            new FrameworkPropertyMetadata(OnWatermarkChanged));

        private static void OnWatermarkChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            WatermarkTextBox txtBox = sender as WatermarkTextBox;
            if (txtBox != null)
            {
                txtBox.GotFocus -= OnTextFocus;
                txtBox.GotFocus += OnTextFocus;
                txtBox.LostFocus -= OnTextFocus;
                txtBox.LostFocus += OnTextFocus;
                txtBox.TextChanged -= OnTextBoxChanged;
                txtBox.TextChanged += OnTextBoxChanged;
                return;
            }

            PasswordBox pwdBox = sender as PasswordBox;
            if (pwdBox != null)
            {
                pwdBox.PasswordChanged -= OnPasswordChanged;
                pwdBox.PasswordChanged += OnPasswordChanged;
                pwdBox.GotFocus -= OnPasswordFocus;
                pwdBox.GotFocus += OnPasswordFocus;
                pwdBox.LostFocus -= OnPasswordFocus;
                pwdBox.LostFocus += OnPasswordFocus;
                return;
            }
        }

        public delegate void TextFocusEventHandler(object sender, EventArgs e);
        public event TextFocusEventHandler TextFocusEvent
        {
            add
            {
                this.AddHandler(TextFocusOnEvent, value);
            }

            remove
            {
                this.RemoveHandler(TextFocusOnEvent, value);
            }
        }
        public static readonly RoutedEvent TextFocusOnEvent = EventManager.RegisterRoutedEvent("TextFocusEvent", RoutingStrategy.Bubble, typeof
        (TextFocusEventHandler), typeof(WatermarkTextBox));


        public delegate void TextLostFocusEventHandler(object sender, EventArgs e);
        public event TextLostFocusEventHandler TextLostFocusEvent
        {
            add
            {
                this.AddHandler(TextLostFocusOnEvent, value);
            }

            remove
            {
                this.RemoveHandler(TextLostFocusOnEvent, value);
            }
        }
        public static readonly RoutedEvent TextLostFocusOnEvent = EventManager.RegisterRoutedEvent("TextLostFocusEvent", RoutingStrategy.Bubble, typeof
        (TextLostFocusEventHandler), typeof(WatermarkTextBox));

        private static void OnTextBoxChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                WatermarkTextBox txtBox = sender as WatermarkTextBox;
                TextBlock watermarkTextBlock = txtBox.Template.FindName("Watermark", txtBox) as TextBlock;
                if (watermarkTextBlock != null)
                {
                    watermarkTextBlock.Visibility = txtBox.Text.Length == 0
             ? Visibility.Visible : Visibility.Hidden;
                }
            }
            catch (Exception)
            {
            }
        }

        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pwdBox = sender as PasswordBox;
            TextBlock watermarkTextBlock = pwdBox.Template.FindName("WatermarkTextBlock", pwdBox) as TextBlock;

            if (watermarkTextBlock != null)
            {
                watermarkTextBlock.Visibility = pwdBox.SecurePassword.Length == 0
         ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private static void OnPasswordFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pwdBox = sender as PasswordBox;
            TextBlock watermarkTextBlock = pwdBox.Template.FindName("WatermarkTextBlock", pwdBox) as TextBlock;

            if (watermarkTextBlock != null)
            {
                if (e.RoutedEvent.Name == "GotFocus")
                {
                    if (pwdBox.SecurePassword.Length > 0)
                        watermarkTextBlock.Visibility = Visibility.Hidden;
                }
                else if (e.RoutedEvent.Name == "LostFocus")
                {
                    if (pwdBox.SecurePassword.Length == 0)
                        watermarkTextBlock.Visibility = Visibility.Visible;
                }
            }
        }

        private static void OnTextFocus(object sender, RoutedEventArgs e)
        {
            WatermarkTextBox txtBox = sender as WatermarkTextBox;
            TextBlock watermarkTextBlock = txtBox.Template.FindName("Watermark", txtBox) as TextBlock;
            if (txtBox.IsFocused)
                txtBox.RaiseEvent(new RoutedEventArgs(TextFocusOnEvent));
            else
                txtBox.RaiseEvent(new RoutedEventArgs(TextLostFocusOnEvent));

            if (watermarkTextBlock != null)
            {
                if (e.RoutedEvent.Name == "GotFocus")
                {
                    if (txtBox.Text.Length == 0)
                        watermarkTextBlock.Visibility = Visibility.Hidden;
                }
                else if (e.RoutedEvent.Name == "LostFocus")
                {
                    if (txtBox.Text.Length == 0)
                        watermarkTextBlock.Visibility = Visibility.Visible;
                }
            }
        }

    }
}
