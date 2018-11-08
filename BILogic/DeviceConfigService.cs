using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILogic
{
    public class DeviceConfigService
    {
        public List<DeviceConfig> getConfigs()
        {
            return new BIDataAccess.DeviceConfigDal().GetDeviceConfigs();
        }

        public bool CreateConfig(DeviceConfig entity)
        {
            return new BIDataAccess.DeviceConfigDal().Create(entity);
        }

        public bool UpdateConfig(DeviceConfig entity)
        {
            return new BIDataAccess.DeviceConfigDal().Update(entity);
        }

        public bool DeleteConfig(DeviceConfig entity)
        {
            return new BIDataAccess.DeviceConfigDal().Delete(entity);
        }
    }
}
