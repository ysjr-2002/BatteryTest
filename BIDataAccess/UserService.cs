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
        public bool Login(string name, string password)
        {
            try
            {
                using (var db = new batteryEntities())
                {
                    var item = db.UserInfoes.FirstOrDefault(s => s.UserName == name && s.Password == password);
                    if (item != null)
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<UserInfo> GetUsers()
        {
            try
            {
                using (var db = new batteryEntities())
                {
                    return db.UserInfoes.ToList();
                }
            }
            catch (Exception ex)
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

        public bool DeleteUser(UserInfo user)
        {
            try
            {
                using (var db = new batteryEntities())
                {
                    var entry = db.Entry<UserInfo>(user);
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

        public bool UpdatePassWord(int userId, string password)
        {
            try
            {
                using (var db = new batteryEntities())
                {
                    var user = db.UserInfoes.First((s) => s.UserID == userId);
                    user.Password = password;
                    var ret = db.SaveChanges();
                    return ret >= 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
