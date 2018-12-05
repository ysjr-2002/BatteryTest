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
    public static class AccessDBHelper
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

        public static async void DeleteInstrumentByHWName(string name)
        {
            var sql1 = "delete from HWCfgInstrument where HWName='{0}'";
            sql1 = string.Format(sql1, name);
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();
                var cmd = new OleDbCommand(sql1, conn);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async void DeleteModelByHWName(string name)
        {
            var sql2 = "delete from HWCfgModel where HWName='{0}'";
            sql2 = string.Format(sql2, name);
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();
                var cmd = new OleDbCommand(sql2, conn);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async void DeleteModuleByHWName(string name)
        {
            var sql3 = "delete from HWCfgModule where HWName='{0}'";
            sql3 = string.Format(sql3, name);
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                conn.Open();
                var cmd = new OleDbCommand(sql3, conn);
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

        /// <summary>
        /// 获取仪器列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<List<HWCfgInstrumentModel>> GetInstrumentModelList(string name)
        {
            List<HWCfgInstrumentModel> list = new List<HWCfgInstrumentModel>();
            var sql = "select * from HWCfgInstrument where HWName='{0}'";
            sql = string.Format(sql, name);
            using (OleDbConnection conn = new OleDbConnection(connection))
            {
                try
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand(sql, conn);
                    DbDataReader reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var model = new HWCfgInstrumentModel
                        {
                            ID = reader["ID"].ToInt32(),
                            InstrumentName = reader["InstrumentName"].ToString(),
                            HWName = reader["HWName"].ToString()
                        };
                        list.Add(model);
                    }
                }
                catch (Exception e)
                {
                }
                return list;
            }
        }

        public static async void Insert(HWCfgInstrumentModel model)
        {
            try
            {
                var sql = "insert into HWCfgInstrument values({0},'{1}', '{2}',{3})";
                using (OleDbConnection conn = new OleDbConnection(connection))
                {
                    conn.Open();
                    sql = string.Format(sql, model.ID, model.InstrumentName, model.HWName, model.Active);
                    var cmd = new OleDbCommand(sql, conn);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {

            }
        }

        public static async void Insert(HWCfgModel model)
        {
            try
            {
                var sql = "insert into HWCfgModel values('{0}','{1}','{2}',{3},'{4}','{5}',{6},{7},'{8}','{9}',{10})";
                using (OleDbConnection conn = new OleDbConnection(connection))
                {
                    conn.Open();
                    sql = string.Format(sql,
                        model.ModelName,
                        model.HWName,
                        model.InstrumentName,
                        model.ModelIndex,
                        model.Interface,
                        model.InterfaceParameter,
                        model.ModulesMax,
                        model.ChannelsPerModule,
                        model.SubDevices,
                        model.ModuleClassify,
                        model.Active
                        );
                    var cmd = new OleDbCommand(sql, conn);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {

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
                    model.ModelIndex = reader["ModelIndex"].ToInt32();
                    model.Interface = reader["Interface"].ToString();
                    model.InterfaceParameter = reader["InterfaceParameter"].ToString();
                    model.ModulesMax = reader["ModulesMax"].ToInt32();
                    model.ChannelsPerModule = reader["ChannelsPerModule"].ToInt32();
                    model.SubDevices = reader["SubDevices"].ToString();
                    model.ModuleClassify = reader["ModuleClassify"].ToString();
                    model.Active = reader["Active"].ToInt32();
                    list.Add(model);
                }
                return list;
            }
        }
    }
}
