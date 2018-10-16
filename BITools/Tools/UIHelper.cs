using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace BITools.Tools
{
    public class UIHelper
    {

        #region 解决Popup内的textBox输入法无法切换中文的问题
        [DllImport("User32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        public static IntPtr GetHwnd(Popup popup)
        {
            HwndSource source = (HwndSource)PresentationSource.FromVisual(popup.Child);
            return source.Handle;
        }
        #endregion

        /// <summary>
        /// 找到指定名字的子元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;
                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                    else
                        foundChild = FindChild<T>(child, childName);
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        /// <summary>
        /// 查找指定类型的父元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="curDp"></param>
        /// <returns></returns>
        public static T FindParent<T>(DependencyObject curDp) where T : DependencyObject
        {
            DependencyObject dobj = (DependencyObject)VisualTreeHelper.GetParent(curDp);
            if (dobj != null)
            {
                if (dobj is T)
                {
                    return (T)dobj;
                }
                else
                {
                    dobj = FindParent<T>(dobj);
                    if (dobj != null && dobj is T)
                    {
                        return (T)dobj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 查找Popup里指定类型的父元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="curDp"></param>
        /// <returns></returns>
        public static T FindParentInPopup<T>(DependencyObject curDp) where T : DependencyObject
        {
            DependencyObject dobj = (DependencyObject)LogicalTreeHelper.GetParent(curDp);
            if (dobj != null)
            {
                if (dobj is T)
                {
                    return (T)dobj;
                }
                else
                {
                    dobj = FindParentInPopup<T>(dobj);
                    if (dobj != null && dobj is T)
                    {
                        return (T)dobj;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 对ObservableCollection按指定要求进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lst">数据源</param>
        /// <param name="SourceIndex"></param>
        /// <param name="TargeIndex"></param>
        /// <returns></returns>
        public static ObservableCollection<T> SortList<T>(ObservableCollection<T> lst, T TargetModel, int TargeIndex = 0, int SourceIndex = 0) where T : new()
        {
            if (lst == null || lst.Count <= 0) return new ObservableCollection<T>();
            var index = lst.IndexOf(TargetModel);
            lst.Remove(TargetModel);
            lst.Insert(TargeIndex, TargetModel);
            return lst;
        }

        public static void BigDataSearchPanelAnimation(ToggleButton btnExpand, Grid GridSearch, Border SearchPanel)
        {
            System.Windows.Media.Animation.DoubleAnimation ta = new System.Windows.Media.Animation.DoubleAnimation();
            ta.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseInOut };

            if (btnExpand.IsChecked == true)
            {
                GridSearch.Visibility = Visibility.Hidden;
                // ta.Completed += (a, b) => { };
                ta.To = 37;
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SearchPanel.BeginAnimation(Border.WidthProperty, ta);
            }
            else
            {
                ta.Completed += (a, b) => { GridSearch.Visibility = Visibility.Visible; };
                ta.To = 315;
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SearchPanel.BeginAnimation(Border.WidthProperty, ta);
            }
        }

        public static void CarSearchPanelAnimation(ToggleButton btnExpand, Grid SecondPanel, Grid rootPanel)
        {
            System.Windows.Media.Animation.DoubleAnimation ta = new System.Windows.Media.Animation.DoubleAnimation();
            ta.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseInOut };

            if (btnExpand.IsChecked == true)
            {
                ta.To = rootPanel.ActualWidth - (11);
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SecondPanel.BeginAnimation(Grid.WidthProperty, ta);

                //if (SearchResultPanel.Children.Count == 1)
                //{
                //    SearchResultPanel.Children.Remove(SearchResult);
                //    SearchResultPanel_Expand.Children.Add(SearchResult);
                //    ReturnSearechPanelBtn.Visibility = Visibility.Collapsed;
                //    SearchResultPanel_Expand.Visibility = Visibility.Visible;
                //}

            }
            else
            {
                ta.Completed += (a, b) =>
                {

                };
                ta.To = 360;
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SecondPanel.BeginAnimation(Grid.WidthProperty, ta);
                //if (SearchResultPanel_Expand.Children.Count == 2)
                //{
                //    SearchResultPanel_Expand.Children.Remove(SearchResult);
                //    SearchResultPanel.Children.Add(SearchResult);
                //    SearchResultPanel_Expand.Visibility = Visibility.Collapsed;
                //    ReturnSearechPanelBtn.Visibility = Visibility.Visible;
                //}
            }
        }

        public static void FaceSearchPanelAnimation(ToggleButton btnExpand, Grid SecondPanel, Grid rootPanel, double width)
        {
            System.Windows.Media.Animation.DoubleAnimation ta = new System.Windows.Media.Animation.DoubleAnimation();
            ta.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseInOut };

            if (btnExpand.IsChecked == true)
            {
                ta.To = rootPanel.ActualWidth - (11 + width);
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SecondPanel.BeginAnimation(Grid.WidthProperty, ta);
            }
            else
            {
                ta.Completed += (a, b) =>
                {

                };
                ta.To = 360;
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SecondPanel.BeginAnimation(Grid.WidthProperty, ta);
            }
        }

        public static void PersonnelDeploymentAnimation(ToggleButton btnExpand, Grid SecondPanel, Grid rootPanel)
        {
            System.Windows.Media.Animation.DoubleAnimation ta = new System.Windows.Media.Animation.DoubleAnimation();
            ta.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseInOut };

            if (btnExpand.IsChecked == true)
            {
                ta.To = rootPanel.ActualWidth - (11);
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SecondPanel.BeginAnimation(Grid.WidthProperty, ta);
            }
            else
            {
                ta.Completed += (a, b) =>
                {

                };
                ta.To = 360;
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SecondPanel.BeginAnimation(Grid.WidthProperty, ta);
            }
        }

        public static void CarDeploymentAnimation(ToggleButton btnExpand, Grid SecondPanel, Grid rootPanel, TabItem tabContent_1, TabItem tabContent_2)
        {
            System.Windows.Media.Animation.DoubleAnimation ta = new System.Windows.Media.Animation.DoubleAnimation();
            ta.EasingFunction = new BackEase() { EasingMode = EasingMode.EaseInOut };

            if (btnExpand.IsChecked == true)
            {
                ta.To = rootPanel.ActualWidth - (11);
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SecondPanel.BeginAnimation(Grid.WidthProperty, ta);
                tabContent_1.Width = rootPanel.ActualWidth / 2 - 10;
                tabContent_2.Width = rootPanel.ActualWidth / 2 - 12;
            }
            else
            {
                ta.Completed += (a, b) =>
                {
                    tabContent_1.Width = 175;
                    tabContent_2.Width = 175;
                };
                ta.To = 360;
                ta.Duration = (Duration)Application.Current.Resources["TInOutDuration"];
                SecondPanel.BeginAnimation(Grid.WidthProperty, ta);

            }
        }

        public static void PersonnelDeploymentMoreSearch(bool Expand, Grid gd)
        {
            System.Windows.Media.Animation.DoubleAnimation ta = new System.Windows.Media.Animation.DoubleAnimation();

            if (Expand)
            {
                ta.To = 264;
                ta.Duration = (Duration)Application.Current.Resources["SmoothDuration"];
                gd.BeginAnimation(Grid.HeightProperty, ta);
            }
            else
            {
                ta.To = 0;
                ta.Duration = (Duration)Application.Current.Resources["SmoothDuration"];
                gd.BeginAnimation(Grid.HeightProperty, ta);

            }
        }

        public static void VideoSynopsisMoreSearch(bool Expand, Grid gd, double Old_AdvanceConditon_heigh)
        {
            System.Windows.Media.Animation.DoubleAnimation ta = new System.Windows.Media.Animation.DoubleAnimation();

            if (Expand)
            {
                ta.To = Old_AdvanceConditon_heigh;
                ta.Duration = (Duration)Application.Current.Resources["SmoothDuration"];
                gd.BeginAnimation(Grid.HeightProperty, ta);
            }
            else
            {
                ta.To = 0;
                ta.Duration = (Duration)Application.Current.Resources["SmoothDuration"];
                gd.BeginAnimation(Grid.HeightProperty, ta);

            }
        }

    }
}
