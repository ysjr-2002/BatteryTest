using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using Xceed.Wpf.Toolkit;
using System.Windows.Input;
using System.Threading;

namespace BITools.UIControls
{

    public enum DateTimePickerFormat { Long, Short, Time, Custom }

    [System.ComponentModel.DefaultBindingProperty("Value")]
    /// <summary>
    /// HZDateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class HZDateTimePicker : UserControl
    {
        private CheckBox _checkBox;
        internal TextBox _textBox;
        private TextBlock _textBlock;
        private Button _calenderBtn;
        private BlockManager _blockManager;
        private string _defaultFormat = "MM/dd/yyyy hh:mm:ss tt";
        bool istimePickEnable = true;
        [Category("Dameer")]
        public bool ShowCheckBox
        {
            get { return this._checkBox.Visibility == System.Windows.Visibility.Visible ? true : false; }
            set
            {
                if (value)
                    this._checkBox.Visibility = System.Windows.Visibility.Visible;
                else
                {
                    this._checkBox.Visibility = System.Windows.Visibility.Collapsed;
                    this.Checked = true;
                }
            }
        }
        //[Category("Dameer")]
        //public bool ShowDropDown
        //{
        //    get { return this._textBlock.Visibility == System.Windows.Visibility.Visible ? true : false; }
        //    set
        //    {
        //        if (value)
        //            this._textBlock.Visibility = System.Windows.Visibility.Visible;
        //        else
        //            this._textBlock.Visibility = System.Windows.Visibility.Collapsed;
        //    }
        //}

        [Category("Dameer")]
        public bool ShowDropDown
        {
            get { return this._calenderBtn.Visibility == System.Windows.Visibility.Visible ? true : false; }
            set
            {
                if (value)
                    this._calenderBtn.Visibility = System.Windows.Visibility.Visible;
                else
                    this._calenderBtn.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        [Category("Dameer")]
        public bool Checked
        {
            get { return this._checkBox.IsChecked.HasValue ? this._checkBox.IsChecked.Value : false; }
            set { this._checkBox.IsChecked = value; }
        }
        [Category("Dameer")]
        private string FormatString
        {
            get
            {
                switch (this.Format)
                {
                    case DateTimePickerFormat.Long:
                        return "dddd, MMMM dd, yyyy";
                    case DateTimePickerFormat.Short:
                        return "M/d/yyyy";
                    case DateTimePickerFormat.Time:
                        return "h:mm:ss tt";
                    case DateTimePickerFormat.Custom:
                        if (string.IsNullOrEmpty(this.CustomFormat))
                            return this._defaultFormat;
                        else
                            return this.CustomFormat;
                    default:
                        return this._defaultFormat;
                }
            }
        }
        private string _customFormat;
        [Category("Dameer")]
        public string CustomFormat
        {
            get { return this._customFormat; }
            set
            {
                this._customFormat = value;
                this._blockManager = new BlockManager(this, this.FormatString);
                var ary = value.Split(' ');
                if (ary.Length == 1)
                {
                    istimePickEnable = false;
                    timer.Visibility = Visibility.Collapsed;
                }
                else
                {
                    switch (ary[1].ToLower())
                    {
                        case "hh:mm":
                            this.timer.CustomFormat = "HH:mm";
                            break;
                        case "hh":
                            this.timer.CustomFormat = "HH";
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private DateTimePickerFormat _format;
        [Category("Dameer")]
        public DateTimePickerFormat Format
        {
            get { return this._format; }
            set
            {
                this._format = value;
                this._blockManager = new BlockManager(this, this.FormatString);
            }
        }
        [Category("Dameer")]
        public DateTime? Text
        {
            get
            {
                if (!this.Checked) return null;
                return (DateTime?)GetValue(ValueProperty);
            }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TheDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Text", typeof(DateTime?), typeof(HZDateTimePicker), new FrameworkPropertyMetadata(DateTime.Now, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(HZDateTimePicker.OnValueChanged), new CoerceValueCallback(HZDateTimePicker.CoerceValue), true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));


        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as HZDateTimePicker)._blockManager.Render();
        }


        public static readonly DependencyProperty WaterMarkProperty = DependencyProperty.Register("WaterMark", typeof(string), typeof(HZDateTimePicker), new PropertyMetadata("", new PropertyChangedCallback(OnWaterMarkChanged)));
        public string WaterMark
        {
            get { return (string)GetValue(WaterMarkProperty); }

            set { SetValue(WaterMarkProperty, value); }
        }

        static void OnWaterMarkChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            var newValue = (string)args.NewValue;
            if (!string.IsNullOrEmpty(newValue))
            {
                HZDateTimePicker source = (HZDateTimePicker)sender;
                source.txtwatermark.Text = (string)args.NewValue;
            }
        }

        static object CoerceValue(DependencyObject d, object value)
        {
            return value;
        }

        internal DateTime InternalValue
        {
            get
            {
                DateTime? value = this.Text;
                if (value.HasValue) return value.Value;
                return DateTime.MinValue;
            }
        }

        public HZDateTimePicker()
        {
            this.Initializ();
            this._blockManager = new BlockManager(this, this.FormatString);
        }

        public delegate void TextBoxValueChangedEventHandler(object sender, RoutedEventArgs e);

        public event TextBoxValueChangedEventHandler TextChangedEvent
        {
            add
            {
                this.AddHandler(TextBoxValueChangedProperty, value);
            }
            remove
            {
                this.RemoveHandler(TextBoxValueChangedProperty, value);
            }
        }
        public static readonly RoutedEvent TextBoxValueChangedProperty = EventManager.RegisterRoutedEvent("TextChangedEvent", RoutingStrategy.Bubble, typeof(TextBoxValueChangedEventHandler), typeof(HZDateTimePicker));
        private void Initializ()
        {
            InitializeComponent();
            //Debug.WriteLine("Initializ");
            //this.Template = GetTemplate();
            this.ApplyTemplate();
            this._checkBox = checkBox;// (CheckBox)this.Template.FindName("checkBox", this);
            this._textBox = textBox;// (TextBox)this.Template.FindName("textBox", this);
                                    /* this._textBlock = textBlock;*/// (TextBlock)this.Template.FindName("textBlock", this);
            this._calenderBtn = btnCalender;
            this._checkBox.Checked += new RoutedEventHandler(_checkBox_Checked);
            this._checkBox.Unchecked += new RoutedEventHandler(_checkBox_Checked);
            this.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(Dameer_MouseWheel);
            this.Focusable = false;
            this._textBox.Cursor = System.Windows.Input.Cursors.Arrow;
            this._textBox.AllowDrop = false;
            this._textBox.GotFocus += new System.Windows.RoutedEventHandler(_textBox_GotFocus);
            this._textBox.LostFocus += new System.Windows.RoutedEventHandler(_textBox_LostFocus); ;
            this._textBox.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(_textBox_PreviewMouseUp);
            this._textBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(_textBox_PreviewKeyDown);
            this._textBox.TextChanged += _textBox_TextChanged;
            this._textBox.ContextMenu = null;
            this._textBox.IsEnabled = this.Checked;
            this._textBox.IsReadOnly = true;
            this._textBox.IsReadOnlyCaretVisible = false;
            //this._textBlock.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(_textBlock_MouseLeftButtonDown);
            this._calenderBtn.Click += _calenderImg_Click; //+= new System.Windows.Input.MouseButtonEventHandler(_textBlock_MouseLeftButtonDown);
            this._textBox.Style = Application.Current.Resources["WPFDateTimePickerStyle"] as Style;
            //this._calendar.SelectedDatesChanged += new EventHandler<SelectionChangedEventArgs>(calendar_SelectedDatesChanged);
            //this._calendar.GotMouseCapture += _calendar_GotMouseCapture;
        }

   

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }
        //private void _calendar_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    //UIElement originalElement = e.OriginalSource as UIElement;
        //    //if (originalElement is System.Windows.Controls.Primitives.CalendarDayButton)
        //    //{
        //    //    originalElement.ReleaseMouseCapture();
        //    //}
        //}

        //private void timer_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    base.OnPreviewMouseUp(e);
        //    if (Mouse.Captured is CalendarItem)
        //    {
        //        Mouse.Capture(null);
        //    }
        //}

        private void _textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxValueChangedProperty != null)
                this.RaiseEvent(new RoutedEventArgs(TextBoxValueChangedProperty));
            if (!string.IsNullOrEmpty(_textBox.Text))
            {
                if(_textBox.Text.Length==4 && _textBox.Text.IndexOf('-')<0)
                {
                    return;
                }
                var time = _textBox.Text.ToDateTime();
                this._calendar.SelectedDate = time;
                this._calendar.DisplayDate = time;
                this.timer.Text = time;
            }

        }

        private void _calenderImg_Click(object sender, RoutedEventArgs e)
        {
            this._popUp.IsOpen = !(this._popUp.IsOpen);
           // this.timer.Value = DateTime.Now;
        }

        void _textBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Debug.WriteLine("_button_Click");
            this._popUp.IsOpen = !(this._popUp.IsOpen);
        }

        void _checkBox_Checked(object sender, RoutedEventArgs e)
        {
            this._textBox.IsEnabled = this.Checked;
        }

        void Dameer_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            //Debug.WriteLine("Dameer_MouseWheel");
            e.Handled = true;
            if (!IsGetFocuse) return;
            if (!string.IsNullOrEmpty(this._textBox.SelectedText))
            {
                this._blockManager.Change(((e.Delta < 0) ? -1 : 1), true);
            }
        }

        bool IsGetFocuse = false;
        void _textBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            //Debug.WriteLine("_textBox_GotFocus");
            IsGetFocuse = true;
            this._blockManager.ReSelect();
        }

        void _textBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            //Debug.WriteLine("_textBox_GotFocus");
            IsGetFocuse = false;
        }
        
        void _textBox_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Debug.WriteLine("_textBox_PreviewMouseUp");
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(10);
                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    this._blockManager.ReSelect();
                }));

            });
        }

        void _textBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //Debug.WriteLine("_textBox_PreviewKeyDown");
            byte b = (byte)e.Key;

            if (e.Key == System.Windows.Input.Key.Left)
                this._blockManager.Left();
            else if (e.Key == System.Windows.Input.Key.Right)
                this._blockManager.Right();
            else if (e.Key == System.Windows.Input.Key.Up)
                this._blockManager.Change(1, true);
            else if (e.Key == System.Windows.Input.Key.Down)
                this._blockManager.Change(-1, true);
            if (b >= 34 && b <= 43)
                this._blockManager.ChangeValue(b - 34);
            if (b >= 74 && b <= 83)
                this._blockManager.ChangeValue(b - 74);
            if (!(e.Key == System.Windows.Input.Key.Tab))
                e.Handled = true;
        }

        //void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        //{
        //this.Checked = true;
        //((sender as Calendar).Parent as Popup).IsOpen = false;
        //DateTime selectedDate = (DateTime)e.AddedItems[0];
        //this.Text = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, this.InternalValue.Hour, this.InternalValue.Minute, this.InternalValue.Second);
        //this._blockManager.Render();
        //}

        //private static ControlTemplate GetTemplate()
        //{
        //    //Debug.WriteLine("GetTemplate");
        //    return (ControlTemplate)XamlReader.Parse(@"
        //<ControlTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        //                  xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
        //    <Border BorderBrush=""Black"" BorderThickness=""1"">
        //        <StackPanel Orientation=""Horizontal"" VerticalAlignment=""Center"" HorizontalAlignment=""Left"" Background=""White"">
        //            <CheckBox Name=""checkBox"" VerticalAlignment=""Center"" />
        //            <TextBox Name=""textBox"" Height=""25"" BorderThickness=""0"" VerticalAlignment=""Center""/>
        //            <TextBlock Name=""textBlock"" Text=""▼""/>
        //        </StackPanel>
        //    </Border>
        //</ControlTemplate>");
        //}

        public override string ToString()
        {
            return this.InternalValue.ToString();
        }

        private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bd.BorderBrush = Application.Current.Resources["TextBox.Border.MouseOver.Brush"] as Brush;
        }
        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bd.BorderBrush = Application.Current.Resources["TextBox.Border.Brush"] as Brush;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _popUp.IsOpen = false;

            DateTime selectedDate = DateTime.Now;
            if (_calendar.SelectedDate != null)
            {
                selectedDate = (DateTime)_calendar.SelectedDate;
            }

            if (istimePickEnable)
            {
                DateTime selectedtime = (DateTime)timer.Text;
                this.Text = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, selectedtime.Hour, selectedtime.Minute, selectedtime.Second);
            }
            else
                this.Text = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, this.InternalValue.Hour, this.InternalValue.Minute, this.InternalValue.Second);


            this._blockManager.Render();
        }


    }

    internal sealed class BlockManager
    {
        internal HZDateTimePicker _dameer;
        private List<Block> _blocks;
        private string _format;
        private Block _selectedBlock;
        private int _selectedIndex;
        public event EventHandler NeglectProposed;
        private string[] _supportedFormats = new string[] {
                "yyyy", "MMMM", "dddd",
                "yyy", "MMM", "ddd",
                "yy", "MM", "dd",
                "y", "M", "d",
                "HH", "H", "hh", "h",
                "mm", "m",
                "ss", "s",
                "tt", "t",
                "fff", "ff", "f",
                "K", "g"};

        public BlockManager(HZDateTimePicker dameer, string format)
        {
            //Debug.WriteLine("BlockManager");
            this._dameer = dameer;
            this._format = format;
            this._dameer.LostFocus += new RoutedEventHandler(_dameer_LostFocus);
            this._blocks = new List<Block>();
            this.InitBlocks();
        }

        private void InitBlocks()
        {
            //Debug.WriteLine("InitBlocks");
            foreach (string f in this._supportedFormats)
                this._blocks.AddRange(this.GetBlocks(f));
            this._blocks = this._blocks.OrderBy((a) => a.Index).ToList();
            this._selectedBlock = this._blocks[0];
            this.Render();
        }

        internal void Render()
        {
            //Debug.WriteLine("BlockManager.Render");
            int accum = 0;
            StringBuilder sb = new StringBuilder(this._format);
            foreach (Block b in this._blocks)
                b.Render(ref accum, sb);
            this._dameer._textBox.Text = this._format = sb.ToString();
            this.Select(this._selectedBlock);
        }

        private List<Block> GetBlocks(string pattern)
        {
            //Debug.WriteLine("GetBlocks");
            List<Block> bCol = new List<Block>();

            int index = -1;
            while ((index = this._format.IndexOf(pattern, ++index)) > -1)
                bCol.Add(new Block(this, pattern, index));
            this._format = this._format.Replace(pattern, (0).ToString().PadRight(pattern.Length, '0'));
            return bCol;
        }

        internal void ChangeValue(int p)
        {
            //Debug.WriteLine("ChangeValue");
            this._selectedBlock.Proposed = p;
            this.Change(this._selectedBlock.Proposed, false);
        }

        internal void Change(int value, bool upDown)
        {
            //Debug.WriteLine("Change");
            this._dameer.Text = this._selectedBlock.Change(this._dameer.InternalValue, value, upDown);
            if (upDown)
                this.OnNeglectProposed();
            this.Render();
        }

        internal void Right()
        {
            //Debug.WriteLine("Right");
            if (this._selectedIndex + 1 < this._blocks.Count)
                this.Select(this._selectedIndex + 1);
        }

        internal void Left()
        {
            //Debug.WriteLine("Left");
            if (this._selectedIndex > 0)
                this.Select(this._selectedIndex - 1);
        }

        private void _dameer_LostFocus(object sender, RoutedEventArgs e)
        {
            this.OnNeglectProposed();
        }

        private void OnNeglectProposed()
        {
            //Debug.WriteLine("OnNeglectProposed");
            EventHandler temp = this.NeglectProposed;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        internal void ReSelect()
        {
            //Debug.WriteLine("ReSelect");
            foreach (Block b in this._blocks)
                if ((b.Index <= this._dameer._textBox.SelectionStart) && ((b.Index + b.Length) >= this._dameer._textBox.SelectionStart))
                { this.Select(b); return; }
            Block bb = this._blocks.Where((a) => a.Index < this._dameer._textBox.SelectionStart).LastOrDefault();
            if (bb == null) this.Select(0);
            else this.Select(bb);
        }

        private void Select(int blockIndex)
        {
            //Debug.WriteLine("Select(int blockIndex)");
            if (this._blocks.Count > blockIndex)
                this.Select(this._blocks[blockIndex]);
        }

        private void Select(Block block)
        {
            //Debug.WriteLine("Select(Block block)");
            if (!(this._selectedBlock == block))
                this.OnNeglectProposed();
            this._selectedIndex = this._blocks.IndexOf(block);
            this._selectedBlock = block;
            this._dameer._textBox.Select(block.Index, block.Length);
        }
    }

    internal class Block
    {
        private BlockManager _blockManager;
        internal string Pattern { get; set; }
        internal int Index { get; set; }
        private int _length;
        internal int Length
        {
            get
            {
                //Debug.WriteLine("Length Get");
                return this._length;
            }
            set
            {
                //Debug.WriteLine("Length Set");
                this._length = value;
            }
        }
        private int _maxLength;
        private string _proposed;
        internal int Proposed
        {
            get
            {
                //Debug.WriteLine(string.Format("Proposed Get, {0}, {1}", this._proposed, this.Length));
                string p = this._proposed;
                return int.Parse(p.PadLeft(this.Length, '0'));
            }
            set
            {
                //Debug.WriteLine(string.Format("Proposed Set, {0}, {1}", this._proposed, this.Length));
                if (!(this._proposed == null) && this._proposed.Length >= this._maxLength)
                    this._proposed = value.ToString();
                else
                    this._proposed = string.Format("{0}{1}", this._proposed, value);
            }
        }

        public Block(BlockManager blockManager, string pattern, int index)
        {
            //Debug.WriteLine("Block");
            this._blockManager = blockManager;
            this._blockManager.NeglectProposed += new EventHandler(_blockManager_NeglectProposed);
            this.Pattern = pattern;
            this.Index = index;
            this.Length = this.Pattern.Length;
            this._maxLength = GetMaxLength(this.Pattern);
        }

        private static int GetMaxLength(string p)
        {
            switch (p)
            {
                case "y":
                case "M":
                case "d":
                case "h":
                case "m":
                case "s":
                case "H":
                    return 2;
                case "yyy":
                    return 4;
                default:
                    return p.Length;
            }
        }

        private void _blockManager_NeglectProposed(object sender, EventArgs e)
        {
            //Debug.WriteLine("_blockManager_NeglectProposed");
            this._proposed = null;
        }

        internal DateTime Change(DateTime dateTime, int value, bool upDown)
        {
            //Debug.WriteLine("Change(DateTime dateTime, int value, bool upDown)");
            if (!upDown && !this.CanChange()) return dateTime;
            int y, m, d, h, n, s;
            y = dateTime.Year;
            m = dateTime.Month;
            d = dateTime.Day;
            h = dateTime.Hour;
            n = dateTime.Minute;
            s = dateTime.Second;

            if (this.Pattern.Contains("y"))
                y = ((upDown) ? dateTime.Year + value : value);
            else if (this.Pattern.Contains("M"))
                m = ((upDown) ? dateTime.Month + value : value);
            else if (this.Pattern.Contains("d"))
                d = ((upDown) ? dateTime.Day + value : value);
            else if (this.Pattern.Contains("h") || this.Pattern.Contains("H"))
                h = ((upDown) ? dateTime.Hour + value : value);
            else if (this.Pattern.Contains("m"))
                n = ((upDown) ? dateTime.Minute + value : value);
            else if (this.Pattern.Contains("s"))
                s = ((upDown) ? dateTime.Second + value : value);
            else if (this.Pattern.Contains("t"))
                h = ((h < 12) ? (h + 12) : (h - 12));

            if (y > 9999) y = 1;
            if (y < 1) y = 9999;
            if (m > 12) m = 1;
            if (m < 1) m = 12;
            if (d > DateTime.DaysInMonth(y, m)) d = 1;
            if (d < 1) d = DateTime.DaysInMonth(y, m);
            if (h > 23) h = 0;
            if (h < 0) h = 23;
            if (n > 59) n = 0;
            if (n < 0) n = 59;
            if (s > 59) s = 0;
            if (s < 0) s = 59;

            return new DateTime(y, m, d, h, n, s);
        }

        private bool CanChange()
        {
            switch (this.Pattern)
            {
                case "MMMM":
                case "dddd":
                case "MMM":
                case "ddd":
                case "g":
                    return false;
                default:
                    return true;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Pattern, this.Index);
        }

        internal void Render(ref int accum, StringBuilder sb)
        {
            //Debug.WriteLine("Block.Render");
            this.Index += accum;

            string f = this._blockManager._dameer.InternalValue.ToString(this.Pattern + ",").TrimEnd(',');
            sb.Remove(this.Index, this.Length);
            sb.Insert(this.Index, f);
            accum += f.Length - this.Length;

            this.Length = f.Length;
        }
    }
}
