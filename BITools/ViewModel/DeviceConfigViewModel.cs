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
using BIDataAccess.entities;
using Newtonsoft.Json;
using BIModel;
using BILogic;
using BITools.ViewModel.Configs;

namespace BITools.ViewModel
{
    class DeviceConfigViewModel : BaseViewModel
    {
        private DeviceConfigService service;
        public DeviceConfigViewModel()
        {
            TCList = new ObservableCollection<Configs.TCViewModel>();
            service = new DeviceConfigService();
        }

        public ICommand LoadConfigCommand { get { return new DelegateCommand(LoadConfig); } }
        public ICommand SaveConfigCommand { get { return new DelegateCommand(SaveConfig); } }
        public ICommand ClearConfigCommand { get { return new DelegateCommand(ClarConfig); } }


        public ICommand AddTCCommand { get { return new DelegateCommand(AddTC); } }
        public ICommand ModifyTCCommand { get { return new DelegateCommand(ModifyTC); } }

        public ICommand AddLayerCommand { get { return new DelegateCommand<Configs.TCViewModel>(AddLayer); } }
        public ICommand ModifyLayerCommand { get { return new DelegateCommand(ModifyLayer); } }

        public ICommand AddUUTCommand { get { return new DelegateCommand<Configs.LayerViewModel>(AddUUT); } }
        public ICommand ModifyUUTCommand { get { return new DelegateCommand(ModifyUUT); } }

        public ICommand AddChannelCommand { get { return new DelegateCommand<Configs.UUTViewModel>(AddChannel); } }
        public ICommand ModifyChannelCommand { get { return new DelegateCommand(ModifyChannel); } }
        //public ICommand ChannelSelectItemCommand { get { return new DelegateCommand<ChannelViewModel>(ModifyChannel); } }

        public ObservableCollection<Configs.TCViewModel> TCList
        {
            get { return this.GetValue(c => c.TCList); }
            set { this.SetValue(c => c.TCList, value); }
        }

        private void LoadConfig()
        {
            var window = new DeviceListWindow();
            var dialog = window.ShowDialog().GetValueOrDefault();
            if (dialog)
            {
                var content = window.ConfigContent;
                TCList = JsonConvert.DeserializeObject<ObservableCollection<Configs.TCViewModel>>(content);
            }
        }

        private void SaveConfig()
        {
            if (this.TCList.Count == 0)
                return;

            var window = new ConfigNameWindow();
            var dialog = window.ShowDialog().GetValueOrDefault();
            if (dialog)
            {
                var content = JsonConvert.SerializeObject(this.TCList);
                DeviceConfig entity = new DeviceConfig();
                entity.Name = window.ConfigName;
                entity.IsDefault = window.IsDefault;
                entity.DeviceContent = content;
                entity.User = AppContext.UserName;
                entity.CreateTime = DateTime.Now;
                service.CreateUser(entity);
                MsgBox.SuccessShow("保存成功！");
            }
        }

        private void ClarConfig()
        {
            var dialog = MsgBox.QuestionShow("确认清空当前配置吗？");
            if (dialog == MsgBoxResult.OK)
            {
                this.TCList.Clear();
            }
        }

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
                        var uut = new Configs.UUTViewModel { Code = i.ToString() };
                        uut.Init();
                        layer.UUTList.Add(uut);
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
            //var window = new ChannelConfigWindow(null);
            //var dialog = window.ShowDialog();
            //if (dialog.GetValueOrDefault())
            //{
            //    uut.ChannelList.Add(window.ChannelInterfaceViewModel);
            //}
        }

        private void ModifyChannel()
        {

        }
    }
}
