using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BITools.Helpers;
using BITools.Model;

namespace BITools.UIControls
{
    /// <summary>
    /// HZTabControlWithCollapse.xaml 的交互逻辑
    /// </summary>
    public partial class HZTabControlWithCollapse : UserControl
    {
        ScrollViewer scrHead;
        ToggleButton tgMenuBtn;
        Popup DropTabMenu;
        ListBox LBTabMenu;
        List<TabControlCollapseMenuModel> lstPopMenu = new List<TabControlCollapseMenuModel>();
        List<TabItem> CollapseMenu = new List<TabItem>();
        public HZTabControlWithCollapse()
        {
            InitializeComponent();
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                return;
            }
            Items = new ObservableCollection<TabItem>();
            RightContainer.ApplyTemplate();
            scrHead = UIHelper.FindChild<ScrollViewer>(RightContainer, "scrol");
            scrHead.ScrollChanged += Xxx_ScrollChanged;
            tgMenuBtn = UIHelper.FindChild<ToggleButton>(RightContainer, "tgBtn");
            // tgMenuBtn.Click += TgMenuBtn_Click;
            DropTabMenu = UIHelper.FindChild<Popup>(RightContainer, "popTabItem");

            LBTabMenu = DropTabMenu.Child as ListBox;

        }

        public ObservableCollection<TabItem> Items
        {
            get { return (ObservableCollection<TabItem>)GetValue(ItemsProperty); }
            set
            {
                SetValue(ItemsProperty, value);
                foreach (var item in value)
                {
                    bool isFindSameObj = false;
                    foreach (var obj in RightContainer.Items)
                    {
                        if (obj as TabItem == item)
                        {
                            isFindSameObj = true;
                            break;
                        }
                    }
                    if (isFindSameObj) continue;

                    foreach (var obj in CollapseMenu)
                    {
                        if (obj == item)
                        {
                            isFindSameObj = true;
                            break;
                        }
                    }
                    if (isFindSameObj) continue;

                    foreach (var itemxxx in RightContainer.Items)
                    {
                        //System.Diagnostics.Debug.WriteLine((itemxxx as TabItem).Name);
                        if ((itemxxx as TabItem).Name == item.Name)
                        {
                            Xxx_ScrollChanged(null, null);
                            isFindSameObj = true;
                        }
                    }
                    if (isFindSameObj) continue;
                    RightContainer.Items.Add(item);
                    item.ApplyTemplate();
                    var btn = UIHelper.FindChild<Button>(item, "btnDel");
                    btn.Click += Btn_Click;
                    btn.Tag = item;
                }
                if (Items.Count > 0)
                {
                    this.Visibility = Visibility.Visible;
                }
                else
                {
                    this.Visibility = Visibility.Hidden;
                }
            }
        }
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<TabItem>), typeof(HZTabControlWithCollapse), new PropertyMetadata(new PropertyChangedCallback(ItemsChanged)));

        private static void ItemsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            HZTabControlWithCollapse control = obj as HZTabControlWithCollapse;
            if (control != null)
                control.HZTabControlWithCollapse_CollectionChanged((ObservableCollection<TabItem>)e.OldValue, (ObservableCollection<TabItem>)e.NewValue);
        }

        private void HZTabControlWithCollapse_CollectionChanged(ObservableCollection<TabItem> oldValue, ObservableCollection<TabItem> newValue)
        {
            var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;

            if (null != oldValueINotifyCollectionChanged)
            {
                oldValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(CollectionChanged_CollectionChanged);
            }

            var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
            if (null != newValueINotifyCollectionChanged)
            {
                Items = newValue;
                newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(CollectionChanged_CollectionChanged);
            }
        }

        void CollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ObservableCollection<TabItem> items = (ObservableCollection<TabItem>)sender;
            Items = items;
        }


        public delegate void SelectionChanged_EventHandler(object sender, EventArgs e);
        public event SelectionChanged_EventHandler SelectionChanged
        {
            add
            {
                this.AddHandler(SelectionChangedEvent, value);
            }

            remove
            {
                this.RemoveHandler(SelectionChangedEvent, value);
            }
        }
        public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(SelectionChanged_EventHandler), typeof(HZTabControlWithCollapse));

        public TabItem SelectedItem
        {
            get { return (TabItem)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.RegisterAttached("SelectedItem", typeof(TabItem), typeof(HZTabControlWithCollapse));


        public bool SetSelectedItem(string TabItemHeader)
        {
            foreach (var tabItem in RightContainer.Items)
            {
                var tab = tabItem as TabItem;
                if (tab.Name == TabItemHeader)
                {
                    tab.IsSelected = true;
                    return true;
                }
            }

            foreach (var item in lstPopMenu)
            {
                if (item.Name == TabItemHeader)
                {
                    NewItem_SlectTabItemClickEvent(item, null);
                    return true;
                }
            }
            var bb = Items;
            return false;
        }


        private void Xxx_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (scrHead.ComputedHorizontalScrollBarVisibility != Visibility.Visible)
            {
                return;
            }

            for (int i = RightContainer.Items.Count - 1; i >= 0; i--)
            {
                var item = RightContainer.Items[i];
                TabItem CurTabItem = (item as TabItem);
                if (CurTabItem.IsSelected)
                    continue;
                else
                {
                    //System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    //{
                    if (CollapseMenu.IndexOf(item as TabItem) < 0)
                    {
                        RightContainer.Items.Remove(item);
                        CollapseMenu.Add(item as TabItem);
                        tgMenuBtn.Visibility = Visibility.Visible;
                        if (lstPopMenu.Where(p => p.Name == CurTabItem.Name).Count() == 0)
                        {
                            var NewItem = new TabControlCollapseMenuModel { Name = CurTabItem.Name, Header = CurTabItem.Header.ToString(), item = CurTabItem };
                            NewItem.RemoveClickEvent += NewItem_RemoveClickEvent;
                            NewItem.SlectTabItemClickEvent += NewItem_SlectTabItemClickEvent;
                            lstPopMenu.Add(NewItem);
                        }
                        else
                        {
                            //System.Diagnostics.Debug.WriteLine("下拉表已经存在=" + CurTabItem.Name);
                        }


                        LBTabMenu.ItemsSource = new List<TabControlCollapseMenuModel>(lstPopMenu);
                        break;
                    }
                }
            }
            foreach (var item in RightContainer.Items)
            {
                var aaa = item;
                //System.Diagnostics.Debug.WriteLine("RightContainer=" + (item as TabItem).Header.ToString());
                for (int i = lstPopMenu.Count - 1; i >= 0; i--)
                {
                    if ((item as TabItem).Name == lstPopMenu[i].item.Name)
                    {
                        lstPopMenu.RemoveAt(i);
                        LBTabMenu.ItemsSource = new List<TabControlCollapseMenuModel>(lstPopMenu);
                        var a = CollapseMenu.Where(p => p.Header.ToString() == (item as TabItem).Header.ToString()).FirstOrDefault();
                        if (a != null)
                        {
                            CollapseMenu.Remove(a);
                        }
                    }
                }
                //foreach (var item2 in lstPopMenu)
                //{
                //    if (item == item2.item)
                //    {
                //        lstPopMenu.Remove(item2);
                //        LBTabMenu.ItemsSource = new List<TabControlCollapseMenuModel>(lstPopMenu);
                //        var a = CollapseMenu.Where(p => p.Header.ToString() == (item as TabItem).Header.ToString()).FirstOrDefault();
                //        if(a!=null)
                //        {
                //            CollapseMenu.Remove(a);
                //        }
                //        break;
                //    }

                //}
            }


        }

        private void NewItem_SlectTabItemClickEvent(object sender, EventArgs e)
        {
            tgMenuBtn.IsChecked = false;
            RightContainer.SelectionChanged -= RightContainer_SelectionChanged;
            int maxCount = RightContainer.Items.Count;
            for (int i = 0; i < maxCount - 1; i++)
            {
                (RightContainer.Items[i] as TabItem).IsSelected = false;
            }

            var temp = RightContainer.Items[maxCount - 1] as TabItem;
            RightContainer.Items.Remove(temp);
            var obj = (sender as TabControlCollapseMenuModel).item;
            RightContainer.Items.Add(obj);
            obj.IsSelected = true;
            RightContainer.SelectionChanged += RightContainer_SelectionChanged;
            RightContainer_SelectionChanged(RightContainer, null);

            lstPopMenu.Remove(sender as TabControlCollapseMenuModel);
            var NewItem = new TabControlCollapseMenuModel { Name = temp.Name, Header = temp.Header.ToString(), item = temp };
            NewItem.RemoveClickEvent += NewItem_RemoveClickEvent;
            NewItem.SlectTabItemClickEvent += NewItem_SlectTabItemClickEvent;
            lstPopMenu.Add(NewItem);
            LBTabMenu.ItemsSource = new List<TabControlCollapseMenuModel>(lstPopMenu);
        }


        private void NewItem_RemoveClickEvent(object sender, EventArgs e)
        {
            TabControlCollapseMenuModel item = sender as TabControlCollapseMenuModel;
            lstPopMenu.Remove(item);
            LBTabMenu.ItemsSource = new List<TabControlCollapseMenuModel>(lstPopMenu);
            if (lstPopMenu.Count == 0)
            {
                tgMenuBtn.IsChecked = false;
                tgMenuBtn.Visibility = Visibility.Collapsed;
            }
            foreach (var obj in CollapseMenu)
            {
                if (obj == item.item)
                {
                    CollapseMenu.Remove(obj);
                    break;
                }
            }
            foreach (var obj in Items)
            {
                if (obj == item.item)
                {
                    Items.Remove(obj);
                    break;
                }
            }
        }

        public void Remove(TabItem tab)
        {
            RightContainer.Items.Remove(tab);
            for (int i = Items.Count - 1; i >= 0; i--)
            {
                if (Items[i].Name == tab.Name)
                {
                    Items.RemoveAt(i);
                }
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            scrHead.ScrollChanged -= Xxx_ScrollChanged;
            foreach (var tabItem in RightContainer.Items)
            {
                var tab = tabItem as TabItem;
                if (tab == (sender as Button).Tag)
                {
                    RightContainer.Items.Remove(tab);
                    for (int i = Items.Count - 1; i >= 0; i--)
                    {
                        if (Items[i].Name == tab.Name)
                        {
                            Items.RemoveAt(i);
                        }
                    }



                    for (int i = RightContainer.Items.Count - 1; i >= 0; i--)
                    {
                        if ((RightContainer.Items[i] as TabItem).Name == tab.Name)
                        {
                            RightContainer.Items.RemoveAt(i);
                        }
                    }

                    foreach (var item in CollapseMenu)
                    {
                        CollapseMenu.Remove(item);
                        bool blIsSameItme = false;
                        foreach (var obj in RightContainer.Items)
                        {
                            if (obj == item)
                                blIsSameItme = true;
                        }
                        if (blIsSameItme == false)
                            RightContainer.Items.Add(item);

                        for (int i = lstPopMenu.Count - 1; i >= 0; i--)
                        {
                            if (lstPopMenu[i].item == item)
                            {
                                lstPopMenu.RemoveAt(i);
                                LBTabMenu.ItemsSource = new List<TabControlCollapseMenuModel>(lstPopMenu);
                                if (lstPopMenu.Count == 0)
                                {
                                    tgMenuBtn.IsChecked = false;
                                    tgMenuBtn.Visibility = Visibility.Collapsed;
                                }
                            }
                        }
                        break;
                    }

                    break;
                }
            }
            scrHead.ScrollChanged += Xxx_ScrollChanged;
        }

        private void RightContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            this.SelectedItem = (sender as TabControl).SelectedItem as TabItem;
            if (SelectionChangedEvent != null)
                this.RaiseEvent(new RoutedEventArgs(SelectionChangedEvent, RightContainer.SelectedItem));
        }
    }
}
