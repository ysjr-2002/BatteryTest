using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    public class UserService
    {
        public List<UserInfo> GetUsers()
        {
            try
            {
                using (var db = new batteryEntities())
                {
                    return db.UserInfoes.ToList();
                }
            }
            catch
            {
                return new List<UserInfo>();
            }
        }

        public bool CreateUser(UserInfo user)
        {
            try
            {
                using (var db = new batteryEntities())
                {
                    db.UserInfoes.Add(user);
                    int ret = db.SaveChanges();
                    return ret >= 1;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUser(UserInfo user)
        {
            try
            {
                using (var db = new batteryEntities())
                {
                    var entry = db.Entry<UserInfo>(user);
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
    }
}
