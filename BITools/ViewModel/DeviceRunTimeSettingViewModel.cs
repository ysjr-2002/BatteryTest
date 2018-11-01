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

namespace BITools.ViewModel
{
    class DeviceRunTimeSettingViewModel : BaseViewModel
    {
        public DeviceRunTimeSettingViewModel()
        {

        }

        public ICommand LoadConfigCommand { get { return new DelegateCommand(LoadConfig); } }
        public ICommand SaveConfigCommand { get { return new DelegateCommand(SaveConfig); } }
        public ICommand ClearConfigCommand { get { return new DelegateCommand(ClarConfig); } }

        public ICommand AddLayerCommand { get { return new DelegateCommand(AddTC); } }

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

            //var window = new ConfigNameWindow();
            //var dialog = window.ShowDialog().GetValueOrDefault();
            //if (dialog)
            //{
            //    var content = JsonConvert.SerializeObject(this.TCList);
            //    DeviceConfig entity = new DeviceConfig();
            //    entity.Name = window.ConfigName;
            //    entity.IsDefault = window.IsDefault;
            //    entity.DeviceContent = content;
            //    entity.User = AppContext.UserName;
            //    entity.CreateTime = DateTime.Now;
            //    service.CreateUser(entity);
            //    MsgBox.SuccessShow("保存成功！");
            //}
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
            var window = new LayerParamWindow();
            window.ShowDialog();
        }
    }
}
