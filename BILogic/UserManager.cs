using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BILogic
{
    public class UserManager
    {
        public List<UserInfo> getUser()
        {
            return new BIDataAccess.UserService().GetUsers();
        }
    }
}
