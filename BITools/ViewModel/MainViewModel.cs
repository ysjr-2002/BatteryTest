using BITools.Core;
using BITools.SystemManager;
using BITools.UIControls;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BITools.ViewModel
{
    /// <summary>
    /// 主
    /// </summary>
    public class MainViewModel : BaseMainViewModel
    {
        public ICommand LoadParamCommand { get { return new DelegateCommand(LoadConfig); } }

        [Inject]
        public Config config { get; set; }

        public override void Loaded()
        {
            base.Loaded();
        }

        private void LoadConfig()
        {
            var filepath = FileManager.OpenParamFile();
            if (filepath.IsEmpty())
                return;

            var content = System.IO.File.ReadAllText(filepath);
            var tcList = JsonConvert.DeserializeObject<ObservableCollection<ViewModel.Configs.TCViewModel>>(content);

            string first = "";
            foreach (var tc in tcList)
            {
                //台车
                if (first.IsEmpty())
                    first = tc.Name;

                TabItem item = new TabItem { Name = tc.Name, Header = tc.Name, IsSelected = true };
                item.DataContext = tc;

                item.Style = Application.Current.Resources["TabItem.TC"] as System.Windows.Style;

                var list = new ListBox();
                list.ItemContainerStyle = Application.Current.FindResource("layerListBoxItem") as Style;
                foreach (var layer in tc.LayerList)
                {
                    //层
                    LayerView temp = new LayerView();
                    LayerViewModel datacontext = new LayerViewModel(filepath, layer, config);
                    datacontext.UUTList = layer.UUTList;
                    datacontext.Refresh();
                    temp.DataContext = datacontext;
                    list.Items.Add(temp);
                }
                item.Content = list;
                TabControl.Items.Add(item);
            }
            TabControl.SetSelectedItem(first);
        }
    }
}
