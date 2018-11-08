using BITools.SystemManager;
using Microsoft.Practices.Prism.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.NotifyBase;
using BIDataAccess.entities;
using BILogic;

namespace BITools.ViewModel
{
    /// <summary>
    /// 设备运行参数
    /// </summary>
    class DeviceRunTimeSettingViewModel : BaseViewModel
    {
        private DeviceConfigService service;
        public DeviceRunTimeSettingViewModel()
        {
            service = new DeviceConfigService();
        }

        public ICommand LoadConfigCommand { get { return new DelegateCommand(LoadConfig); } }
        public ICommand SaveConfigCommand { get { return new DelegateCommand(SaveConfig); } }
        public ICommand ClearConfigCommand { get { return new DelegateCommand(ClarConfig); } }

        public ICommand AddLayerCommand { get { return new DelegateCommand(AddTC); } }
        public ICommand SaveChannelCommand { get { return new DelegateCommand(Saveabc); } }

        public ICommand EditLayerCommand { get { return new DelegateCommand<Configs.LayerViewModel>(ModifyLayer); } }
        public ICommand DeleteLayerCommad { get { return new DelegateCommand<Configs.LayerViewModel>(DeleteLayer); } }

        public ObservableCollection<Configs.TCViewModel> TCList
        {
            get { return this.GetValue(c => c.TCList); }
            set { this.SetValue(c => c.TCList, value); }
        }

        private DeviceConfig currentConfig;

        private void LoadConfig()
        {
            var window = new DeviceListWindow();
            var dialog = window.ShowDialog().GetValueOrDefault();
            if (dialog)
            {
                currentConfig = window.DeviceConfig;
                TCList = JsonConvert.DeserializeObject<ObservableCollection<Configs.TCViewModel>>(currentConfig.DeviceContent);
            }
        }

        private void SaveConfig()
        {
            if (this.TCList.Count == 0)
                return;

            if (currentConfig != null)
            {
                var content = JsonConvert.SerializeObject(this.TCList);
                currentConfig.DeviceContent = content;
                service.UpdateConfig(currentConfig);
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
            return;
            var window = new LayerParamWindow(null);
            window.ShowDialog();
        }

        private void ModifyLayer(Configs.LayerViewModel layer)
        {
            var window = new LayerParamWindow(layer);
            var dialog = window.ShowDialog().GetValueOrDefault();
            if (dialog)
            {
                if (window.IsAllLayer)
                {
                    foreach (var item in TCList.First().LayerList)
                    {
                        item.LHSJ = layer.LHSJ;
                    }
                }
            }
        }

        private void DeleteLayer(Configs.LayerViewModel layer)
        {

        }

        private void Saveabc()
        {

        }
    }
}
