using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using BILogic;

namespace BITools.ViewModel.Configs
{
    /// <summary>
    /// 设备选取
    /// </summary>
    class DeviceListViewModel : BaseViewModel
    {
        private DeviceConfigService service;

        public DeviceListViewModel()
        {
            service = new DeviceConfigService();
        }

        public ObservableCollection<DeviceConfig> DeviceConfigList
        {
            get { return this.GetValue(c => c.DeviceConfigList); }
            set { this.SetValue(c => c.DeviceConfigList, value); }
        }

        public override void Loaded()
        {
            base.Loaded();

            DeviceConfigList = new ObservableCollection<DeviceConfig>(service.getConfigs());
        }
    }
}
