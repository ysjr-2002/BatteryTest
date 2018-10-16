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
            using (var db = new batteryEntities())
            {
                return db.UserInfoes.ToList();
            }
        }
    }
}
