using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIFileParam
{
    public static class DBFactory
    {
        private static readonly string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BIHWconfig.mdb";

        public static System.Data.IDbConnection Create()
        {
            return new OleDbConnection(connection);
        }
    }
}
