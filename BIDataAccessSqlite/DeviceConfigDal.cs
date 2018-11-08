using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    public class DeviceConfigDal
    {
        public List<DeviceConfig> GetDeviceConfigs()
        {
            try
            {
                using (var db = BatteryDBContext.GetConnect())
                {
                    return db.DeviceConfig.ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<DeviceConfig>();
            }
        }

        public bool Create(DeviceConfig entity)
        {
            try
            {
                using (var db = BatteryDBContext.GetConnect())
                {
                    db.DeviceConfig.Add(entity);
                    int ret = db.SaveChanges();
                    return ret >= 1;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Update(DeviceConfig entity)
        {
            try
            {
                using (var db = BatteryDBContext.GetConnect())
                {
                    var entry = db.Entry<DeviceConfig>(entity);
                    entry.State = System.Data.Entity.EntityState.Modified;
                    var ret = db.SaveChanges();
                    return ret >= 1;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(DeviceConfig entity)
        {
            try
            {
                using (var db = BatteryDBContext.GetConnect())
                {
                    var entry = db.Entry<DeviceConfig>(entity);
                    entry.State = System.Data.Entity.EntityState.Deleted;
                    var ret = db.SaveChanges();
                    return ret >= 1;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
