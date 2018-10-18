using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILogic
{
    public class UserImpl
    {
        public Task<bool> Login(string name, string password)
        {
            return Task.Factory.StartNew(() =>
            {
                return new BIDataAccess.UserService().Login(name, password);
            });
        }

        public List<UserInfo> getUser()
        {
            return new BIDataAccess.UserService().GetUsers();
        }

        public bool CreateUser(UserInfo user)
        {
            return new BIDataAccess.UserService().CreateUser(user);
        }

        public bool UpdateUser(UserInfo user)
        {
            return new BIDataAccess.UserService().UpdateUser(user);
        }

        public bool DeleteUser(UserInfo user)
        {
            return new BIDataAccess.UserService().DeleteUser(user);
        }

        public bool UpdatePassWord(int userId, string password)
        {
            return new BIDataAccess.UserService().UpdatePassWord(userId, password);
        }
    }
}
