using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    public class DBConfig
    {
        public static string DataBaseConnectionString()
        {
            string DataSource = ConfigurationManager.AppSettings["Server"].ToString();
            string DataBase = ConfigurationManager.AppSettings["DataBase"].ToString();
            string UserId = ConfigurationManager.AppSettings["User"].ToString();
            string PassWord = ConfigurationManager.AppSettings["Password"].ToString();
            return ConnectionString(DataSource, DataBase, UserId, PassWord);
        }

        public static string ConnectionString(string DataSource, string DataBase, string UserId, string PassWord)
        {
            return string.Concat("metadata=res://*/entities.Model1.csdl|res://*/entities.Model1.ssdl|res://*/entities.Model1.msl;provider=System.Data.SQLite.EF6;provider connection string=\"data source=data.bin\"");
        }
    }
}
