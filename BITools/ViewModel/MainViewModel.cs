using BITools.Core;
using BITools.SystemManager;
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
using System.Windows.Input;

namespace BITools.ViewModel
{
    /// <summary>
    /// 主
    /// </summary>
    public class MainViewModel : BaseMainViewModel
    {
        public int TabSelectedIndex
        {
            get { return this.GetValue(c => c.TabSelectedIndex); }
            set { this.SetValue(c => c.TabSelectedIndex, value); }
        }

        public ICommand TabSelectedCommand { get { return new DelegateCommand<string>(SwitchTab); } }

        public MainViewModel()
        {
            DeviceTestViewModelA = new TCViewModel();
            DeviceTestViewModelB = new TCViewModel();
            DeviceTestViewModelC = new TCViewModel();
            DeviceTestViewModelD = new TCViewModel();
            DeviceTestViewModelE = new TCViewModel();
            DeviceTestViewModelF = new TCViewModel();
            TabSelectedIndex = 0;
        }

        [Inject]
        public Config config { get; set; }

        private void SwitchTab(string tab)
        {
            if (tab == "A")
                TabSelectedIndex = 0;
            if (tab == "B")
                TabSelectedIndex = 1;
            if (tab == "C")
                TabSelectedIndex = 2;
            if (tab == "D")
                TabSelectedIndex = 3;
            if (tab == "E")
                TabSelectedIndex = 4;
            if (tab == "F")
                TabSelectedIndex = 5;
        }

        public override void Loaded()
        {
            base.Loaded();
            var filename = "temp.json";
            if (System.IO.File.Exists(filename))
            {
                var content = System.IO.File.ReadAllText(filename);
                var list = JsonConvert.DeserializeObject<ObservableCollection<ViewModel.Configs.TCViewModel>>(content);

                //var A = list.FirstOrDefault();
                //if (A == null)
                //    return;
            }
        }
    }
}
