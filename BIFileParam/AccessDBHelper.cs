using BIModel.Access;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BICommon;

namespace BIFileParam
{
    /// <summary>
    /// Access连接器
    /// </summary>
    public class AccessDBHelper
    {
        static readonly string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=HWconfig.mdb";

        /// <summary>
        /// 文件列表
        /// </summary>
        /// <returns></returns>
        public static async Task<List<HWCfgFileModel>> CfgList()
        {
            List<HWCfgFileModel> list = new List<HWCfgFileModel>();
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("select * from HWCfgFileList", conn);
                DbDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var model = new HWCfgFileModel();
                    model.HWName = reader["HWName"].ToString();
                    model.HWTime = reader["HWTime"].ToString().ToDateTime();
                    model.Author = reader["Author"].ToString();
                    model.SystemDefault = Convert.ToBoolean(reader["SystemDefault"]);
                    list.Add(model);
                }
                return list;
            }
        }

        public static async void SaveFile(HWCfgFileModel model)
        {
            var sql = "insert into HWCfgFileList() values('{0}','{0}','{0}','{0}','{0}')";
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();
                sql = string.Format(sql, model);
                var cmd = new OleDbCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// 仪器列表
        /// </summary>
        /// <returns></returns>
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

        public static async Task<List<HWCfgModel>> HWCfgModelList()
        {
            List<HWCfgModel> list = new List<HWCfgModel>();
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("select * from HWCfgModel", conn);
                DbDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var model = new HWCfgModel();
                    model.ModelName = reader["ModelName"].ToString();
                    model.HWName = reader["HWName"].ToString();
                    model.InstrumentName = reader["InstrumentName"].ToString();
                    model.ModelIndex = reader["ModelIndex"].ToString();
                    model.Interface = reader["Interface"].ToString();
                    model.InterfaceParameter = reader["InterfaceParameter"].ToString();
                    model.ModulesMax = reader["ModulesMax"].ToString();
                    model.ChannelsPerModule = reader["ChannelsPerModule"].ToString();
                    model.SubDevices = reader["SubDevices"].ToString();
                    model.ModuleClassify = reader["ModuleClassify"].ToString();
                    model.Active = reader["Active"].ToString() == "0";
                    list.Add(model);
                }
                return list;
            }
        }
    }
}
