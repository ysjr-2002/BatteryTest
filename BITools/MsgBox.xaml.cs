using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System;

namespace BITools
{
    /// <summary>
    /// 指定显示在消息框上的按钮
    /// </summary>
    public enum MsgBoxButton
    {
        /// <summary>
        /// 消息框显示 “确定”按钮。
        /// </summary>
        OK,
        /// <summary>
        /// 消息框显示 “确定”和 “取消”按钮。
        /// </summary>
        OKCancel,
        ///// <summary>
        ///// 消息框包含“是”、“否”和“取消”按钮。
        ///// </summary>
        //YesNoCancel,
        ///// <summary>
        ///// 消息框显示 “是”和 “否”按钮。
        ///// </summary>
        //YesNo
    }
    /// <summary>
    /// 指定消息框所显示的图标
    /// </summary>
    public enum MsgBoxImage
    {
        /// <summary>
        /// 不显示图标。
        /// </summary>
        None,
        /// <summary>
        /// 消息框显示一个 “问号”图标。
        /// </summary>
        Question,
        /// <summary>
        /// 消息框显示一个 “警告”图标。
        /// </summary>
        Warning,
        /// <summary>
        /// 消息框显示一个 “错误”图标。
        /// </summary>
        Error,
        /// <summary>
        /// 消息框显示一个 “信息”图标。
        /// </summary>
        Information
    }
    /// <summary>
    /// 指定用户单击的消息框按钮。
    /// </summary>
    public enum MsgBoxResult
    {
        /// <summary>
        /// 消息框未返回值。
        /// </summary>
        None,
        /// <summary>
        /// 消息框的结果值为 “确定”。
        /// </summary>
        OK,
        /// <summary>
        /// 消息框的结果值为 “取消”。
        /// </summary>
        Cancel,
    }
    /// <summary>
    /// MsgBox.xaml 的交互逻辑
    /// </summary>
    public partial class MsgBox : Window
    {
        IntPtr ActiveWindowHandle;  //定义活动窗体的句柄  

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern IntPtr GetActiveWindow();  //获得父窗体句柄  

        MsgBoxResult msgResult = MsgBoxResult.None;
        Storyboard sbLoadAnimat, sbCloseAnimat;

        MsgBox()
        {
            InitializeComponent();
            Topmost = true;
            sbLoadAnimat = new Storyboard();
            DoubleAnimation doubleAnimaion1 = new DoubleAnimation();
            doubleAnimaion1.From = 0;
            doubleAnimaion1.To = 1;
            doubleAnimaion1.Duration = TimeSpan.FromMilliseconds(500);
            Storyboard.SetTarget(doubleAnimaion1, this);
            Storyboard.SetTargetProperty(doubleAnimaion1, new PropertyPath("Opacity"));
            sbLoadAnimat.Children.Add(doubleAnimaion1);
            sbLoadAnimat.Begin();

            sbCloseAnimat = new Storyboard();
            DoubleAnimation doubleAnimaion2 = new DoubleAnimation();
            doubleAnimaion2.From = 1;
            doubleAnimaion2.To = 0;
            doubleAnimaion2.Duration = TimeSpan.FromMilliseconds(500);
            Storyboard.SetTarget(doubleAnimaion2, this);
            Storyboard.SetTargetProperty(doubleAnimaion2, new PropertyPath("Opacity"));
            sbCloseAnimat.Children.Add(doubleAnimaion2);
            sbCloseAnimat.Completed += (a, b) => { this.Close(); };
        }


        /// <summary>
        /// 显示一个询问消息框
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="message">消息内容</param>
        /// <returns>MsgBoxResult对象</returns>
        public static MsgBoxResult QuestionShow(string Title, string message)
        {
            return (MsgBoxResult)Application.Current.Dispatcher.Invoke(new Func<MsgBoxResult>(() =>
            {
                MsgBox msgbox = new MsgBox();
                FormattedText ft = new FormattedText(message,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(msgbox.txtbMessage.FontFamily, msgbox.txtbMessage.FontStyle, msgbox.txtbMessage.FontWeight, msgbox.txtbMessage.FontStretch, msgbox.txtbMessage.FontFamily),
                    msgbox.txtbMessage.FontSize,
                    Brushes.Black);
                msgbox.myTitle.Text = "确认";
                msgbox.txtbMessage.Text = message;
                msgbox.btnOK.Visibility = Visibility.Visible;
                msgbox.btnCancel.Visibility = Visibility.Visible;
                //msgbox.btnCancel.Focus();
                msgbox.imgIcon.Source = new BitmapImage(new Uri("/BITools;component/Images/icon_asking.png", UriKind.Relative));
                msgbox.ActiveWindowHandle = GetActiveWindow();  //获取父窗体句柄  
                WindowInteropHelper helper = new WindowInteropHelper(msgbox);
                helper.Owner = msgbox.ActiveWindowHandle;
                msgbox.ShowInTaskbar = true;
                msgbox.ShowDialog();
                return msgbox.msgResult;
            }));
        }


        /// <summary>
        /// 显示一个警告消息框
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="message">消息内容</param>
        /// <returns>MsgBoxResult对象</returns>
        public static MsgBoxResult WarningShow(string message, string Title = "警告", MsgBoxButton MsgBoxButton = MsgBoxButton.OK)
        {
            if (Application.Current == null) return MsgBoxResult.Cancel;

            return (MsgBoxResult)Application.Current.Dispatcher.Invoke(new Func<MsgBoxResult>(() =>
            {
                MsgBox msgbox = new MsgBox();
                FormattedText ft = new FormattedText(message,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(msgbox.txtbMessage.FontFamily, msgbox.txtbMessage.FontStyle, msgbox.txtbMessage.FontWeight, msgbox.txtbMessage.FontStretch, msgbox.txtbMessage.FontFamily),
                    msgbox.txtbMessage.FontSize,
                    Brushes.Black);
                msgbox.myTitle.Text = Title;
                msgbox.txtbMessage.Text = message;
                msgbox.btnOK.Visibility = Visibility.Visible;
                if (MsgBoxButton == MsgBoxButton.OKCancel)
                    msgbox.btnCancel.Visibility = Visibility.Visible;
                else
                    msgbox.btnCancel.Visibility = Visibility.Collapsed;
                //msgbox.btnCancel.Focus();
                msgbox.imgIcon.Source = new BitmapImage(new Uri("/BITools;component/Images/icon_warn.png", UriKind.Relative)); ;
                msgbox.ActiveWindowHandle = GetActiveWindow();  //获取父窗体句柄  
                WindowInteropHelper helper = new WindowInteropHelper(msgbox);
                helper.Owner = msgbox.ActiveWindowHandle;
                msgbox.ShowInTaskbar = true;
                msgbox.ShowDialog();
                return msgbox.msgResult;
            }));
        }
        /// <summary>
        /// 显示一个成功消息框
        /// </summary>
        /// <param name="Title"></param>
        /// <param name="message"></param>
        /// <param name="MsgBoxButton"></param>
        /// <returns></returns>
        public static MsgBoxResult SuccessShow(string Title, string message, MsgBoxButton MsgBoxButton = MsgBoxButton.OK)
        {
            if (Application.Current == null) return MsgBoxResult.Cancel;

            return (MsgBoxResult)Application.Current.Dispatcher.Invoke(new Func<MsgBoxResult>(() =>
            {
                MsgBox msgbox = new MsgBox();
                FormattedText ft = new FormattedText(message,
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    new Typeface(msgbox.txtbMessage.FontFamily, msgbox.txtbMessage.FontStyle, msgbox.txtbMessage.FontWeight, msgbox.txtbMessage.FontStretch, msgbox.txtbMessage.FontFamily),
                    msgbox.txtbMessage.FontSize,
                    Brushes.Black);
                msgbox.myTitle.Text = "提示";
                msgbox.txtbMessage.Text = message;
                msgbox.btnOK.Visibility = Visibility.Visible;
                if (MsgBoxButton == MsgBoxButton.OKCancel)
                    msgbox.btnCancel.Visibility = Visibility.Visible;
                else
                    msgbox.btnCancel.Visibility = Visibility.Collapsed;
                //msgbox.btnCancel.Focus();
                msgbox.imgIcon.Source = new BitmapImage(new Uri("/BITools;component/Images/icon_done.png", UriKind.Relative)); ;
                msgbox.ActiveWindowHandle = GetActiveWindow();  //获取父窗体句柄  
                WindowInteropHelper helper = new WindowInteropHelper(msgbox);
                helper.Owner = msgbox.ActiveWindowHandle;
                msgbox.ShowInTaskbar = true;
                msgbox.ShowDialog();
                return msgbox.msgResult;
            }));
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            msgResult = MsgBoxResult.None;
            sbCloseAnimat.Begin();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            msgResult = MsgBoxResult.OK;
            sbCloseAnimat.Begin();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            msgResult = MsgBoxResult.Cancel;
            sbCloseAnimat.Begin();
        }
    }
}
