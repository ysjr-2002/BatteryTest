using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using BITools.SystemManager;
using System.Collections.ObjectModel;

namespace BITools.ViewModel
{
    class DeviceConfigViewModel : BaseViewModel
    {
        public DeviceConfigViewModel()
        {
            TCList = new ObservableCollection<Configs.TCViewModel>();
        }

        public ICommand AddTCCommand { get { return new DelegateCommand(AddTC); } }
        public ICommand ModifyTCCommand { get { return new DelegateCommand(ModifyTC); } }

        public ICommand AddLayerCommand { get { return new DelegateCommand<Configs.TCViewModel>(AddLayer); } }
        public ICommand ModifyLayerCommand { get { return new DelegateCommand(ModifyLayer); } }

        public ICommand AddUUTCommand { get { return new DelegateCommand<Configs.LayerViewModel>(AddUUT); } }
        public ICommand ModifyUUTCommand { get { return new DelegateCommand(ModifyUUT); } }

        public ICommand AddChannelCommand { get { return new DelegateCommand<Configs.UUTViewModel>(AddChannel); } }
        public ICommand ModifyChannelCommand { get { return new DelegateCommand(ModifyChannel); } }

        public ObservableCollection<Configs.TCViewModel> TCList
        {
            get { return this.GetValue(c => c.TCList); }
            set { this.SetValue(c => c.TCList, value); }
        }

        //public Configs.TCViewModel TCSelectedItem
        //{
        //    get { return this.GetValue(c => c.TCSelectedItem); }
        //    set { this.SetValue(c => c.TCSelectedItem, value); }
        //}

        private void AddTC()
        {
            var window = new TCConfigWindow();
            var dialog = window.ShowDialog();
            if (dialog.GetValueOrDefault())
            {
                TCList.Add(window.TCViewModel);
            }
        }

        private void ModifyTC()
        {

        }

        private void AddLayer(Configs.TCViewModel tc)
        {
            var window = new LayerConfigWindow();
            var dialog = window.ShowDialog();
            if (dialog.GetValueOrDefault())
            {
                var max = window.MaxLayer.ToInt32();
                if (max > 0)
                {
                    for (int i = 1; i <= max; i++)
                    {
                        tc.LayerList.Add(new Configs.LayerViewModel { Code = "" + i, Name = "L" + i, LHSJ = "1" });
                    }
                }
                else
                {
                    tc.LayerList.Add(window.LayerViewModel);
                }
            }
        }

        private void ModifyLayer()
        {

        }

        private void AddUUT(Configs.LayerViewModel layer)
        {
            var window = new UUTConfigWindow();
            var dialog = window.ShowDialog();
            if (dialog.GetValueOrDefault())
            {
                int max = window.MaxCode.ToInt32();
                if (max > 0)
                {
                    for (int i = 1; i <= max; i++)
                    {
                        layer.UUTList.Add(new Configs.UUTViewModel { Code = i.ToString() });
                    }
                }
                else
                {
                    layer.UUTList.Add(window.UUTViewModel);
                }
            }
        }

        private void ModifyUUT()
        {

        }

        private void AddChannel(Configs.UUTViewModel uut)
        {
            var window = new ChannelConfigWindow();
            var dialog = window.ShowDialog();
            if (dialog.GetValueOrDefault())
            {
                uut.ChannelList.Add(window.ChannelViewModel);
            }
        }

        private void ModifyChannel()
        {

        }
    }
}
