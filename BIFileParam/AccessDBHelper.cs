using BIModel.Access;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIFileParam
{
    /// <summary>
    /// 
    /// </summary>
    public class AccessDBHelper
    {
        static readonly string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=HWconfig.mdb";

        public static async Task<List<InstrumentModel>> InstrumentList()
        {
            List<InstrumentModel> list = new List<InstrumentModel>();
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("select * from InstrumentList", conn);
                DbDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    list.Add(new InstrumentModel { InstrumentName = reader["InstrumentName"].ToString() });
                }
                return list;
            }
        }
    }
}
