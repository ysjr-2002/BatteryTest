using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILogic
{
    public class UserService
    {
        public Task<UserInfo> Login(string name, string password)
        {
            return Task.Factory.StartNew(() =>
            {
                return new BIDataAccess.UserDal().Login(name, password);
            });
        }

        public List<UserInfo> getUser()
        {
            return new BIDataAccess.UserDal().GetUsers();
        }

        public bool CreateUser(UserInfo user)
        {
            return new BIDataAccess.UserDal().CreateUser(user);
        }

        public bool UpdateUser(UserInfo user)
        {
            return new BIDataAccess.UserDal().UpdateUser(user);
        }

        public bool DeleteUser(UserInfo user)
        {
            return new BIDataAccess.UserDal().DeleteUser(user);
        }

        public bool UpdatePassWord(int userId, string password)
        {
            return new BIDataAccess.UserDal().UpdatePassWord(userId, password);
        }
    }
}
