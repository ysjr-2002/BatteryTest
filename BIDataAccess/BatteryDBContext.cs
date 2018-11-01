using BIDataAccess.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    class BatteryDBContext
    {
        static string connectionstring;
        static BatteryDBContext()
        {
            connectionstring = DBConfig.DataBaseConnectionString();
        }

        //public batteryEntities(string test) : base(test)
        //{
        //}

        public static batteryEntities GetConnect()
        {
            return new batteryEntities(connectionstring);
        }
    }
}
