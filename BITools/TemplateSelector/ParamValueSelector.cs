using BIDataAccess.entities;
using BILogic;
using BITools.Enums;
using BITools.Helpers;
using BITools.ViewModel.Configs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BITools.TemplateSelector
{
    /// <summary>
    /// https://www.cnblogs.com/damon-xu/archive/2017/02/08/6376900.html
    /// </summary>
    public class ParamValueSelector : DataTemplateSelector
    {
        public DataTemplate FirstTemplate
        {
            get;
            set;
        }
        public DataTemplate SecondTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;

            var parent = element.Parent as FrameworkElement;
            var dc = parent.DataContext as MonitorParamViewModel;
            if (dc.ValType == (int)ValTypeEnum.Selector)
            {
                var dictImpl = new DictonaryService();
                var list = dictImpl.QueryDictionary("CSMS");
                var datatemplate = element.FindResource("dtSelector") as DataTemplate;
                FrameworkElement fe = datatemplate.LoadContent() as FrameworkElement;
                var combobox = UIHelper.FindChild<ComboBox>(fe, "cmbCSMS");
                if (combobox != null)
                {
                    foreach (Dictonary dict in list)
                    {
                        combobox.Items.Add(new ComboBoxItem { Content = dict.Name });
                    }
                }
                return datatemplate;
            }
            else if (dc.ValType == (int)ValTypeEnum.Input)
            {
                return element.FindResource("dtInput") as DataTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }
}
