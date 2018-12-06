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
    /// Access访问类
    /// </summary>
    public static class AccessDBHelper
    {
        private static readonly string connection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BIHWconfig.mdb";

        /// <summary>
        /// 文件列表
        /// </summary>
        /// <returns></returns>
        public static async Task<List<HWCfgFileModel>> CfgList()
        {
            List<HWCfgFileModel> list = new List<HWCfgFileModel>();
            using (var conn = new OleDbConnection(connection))
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
            using (var conn = new OleDbConnection(connection))
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
            using (var conn = new OleDbConnection(connection))
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
            using (var conn = new OleDbConnection(connection))
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
            using (var conn = new OleDbConnection(connection))
            {
                conn.Open();
                var cmd = new OleDbCommand(sql3, conn);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async void DeleteModel(string name, string instrumentname)
        {
            var sql = "delete from HWCfgModel where HWName='{0}' and InstrumentName='{1}'";
            sql = string.Format(sql, name, instrumentname);
            using (var conn = new OleDbConnection(connection))
            {
                conn.Open();
                var cmd = new OleDbCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async void DeleteModule(string name, string instrumentname)
        {
            var sql = "delete from HWCfgModule where HWName='{0}' and InstrumentName='{1}'";
            sql = string.Format(sql, name, instrumentname);
            using (var conn = new OleDbConnection(connection))
            {
                conn.Open();
                var cmd = new OleDbCommand(sql, conn);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static async void DeleteModel(HWCfgModel model)
        {
            var sql = "delete from HWCfgModel where HWName='{0}' and InstrumentName='{1}' and ModelName='{2}' and ModelIndex={3}";
            sql = string.Format(sql, model.HWName, model.InstrumentName, model.ModelName, model.ModelIndex);
            using (var conn = new OleDbConnection(connection))
            {
                conn.Open();
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
            using (var conn = new OleDbConnection(connection))
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
            using (var conn = new OleDbConnection(connection))
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
                using (var conn = new OleDbConnection(connection))
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
                using (var conn = new OleDbConnection(connection))
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

        /// <summary>
        /// 根据HW名称获取Model
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Task<List<HWCfgModel>> HWCfgModelListByHWName(string name)
        {
            var condition = " where HWName='" + name + "'";
            return HWCfgModelList(condition);
        }

        public static Task<List<HWCfgModel>> HWCfgModelListByInstrumentName(string hwname, string instrumentname)
        {
            var condition = " where HWName='" + hwname + "' and InstrumentName='" + instrumentname + "'";
            return HWCfgModelList(condition);
        }

        private static async Task<List<HWCfgModel>> HWCfgModelList(string condition)
        {
            List<HWCfgModel> list = new List<HWCfgModel>();
            using (var conn = new OleDbConnection(connection))
            {
                conn.Open();
                var sql = "select * from HWCfgModel " + condition;
                var cmd = new OleDbCommand(sql, conn);
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var model = new HWCfgModel();
                    model.HWName = reader["HWName"].ToString();
                    model.InstrumentName = reader["InstrumentName"].ToString();
                    model.ModelName = reader["ModelName"].ToString();
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

        public static Task<List<HWCfgModule>> HWCfgModuleListByInstrumentName(string hwname, string instrumentname)
        {
            var condition = " where HWName='{0}' and InstrumentName='{1}'";
            condition = string.Format(condition, hwname, instrumentname);
            return HWCfgModuleList(condition);
        }

        public static Task<List<HWCfgModule>> HWCfgModuleListByInstrumentName(string hwname, string instrumentname, string modelname)
        {
            var condition = " where HWName='{0}' and InstrumentName='{1}' and ModelName='{2}'";
            condition = string.Format(condition, hwname, instrumentname, modelname);
            return HWCfgModuleList(condition);
        }

        private static async Task<List<HWCfgModule>> HWCfgModuleList(string condition)
        {
            List<HWCfgModule> list = new List<HWCfgModule>();
            using (var conn = new OleDbConnection(connection))
            {
                conn.Open();
                var sql = "select * from HWCfgModule " + condition;
                var cmd = new OleDbCommand(sql, conn);
                var reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    var model = new HWCfgModule();
                    model.HWName = reader["HWName"].ToString();
                    model.InstrumentName = reader["InstrumentName"].ToString();
                    model.ModelName = reader["ModelName"].ToString();
                    model.ModelIndex = reader["ModelIndex"].ToInt32();
                    model.ModuleClassify = reader["ModuleClassify"].ToString();
                    model.Channel = reader["Channel"].ToInt32();
                    model.SpecifiedIndex = reader["SpecifiedIndex"].ToInt32();
                    model.ModuleNo = reader["ModuleNo"].ToInt32();
                    model.ModuleName = reader["ModuleName"].ToString();
                    model.ModulesOccupy = reader["ModulesOccupy"].ToInt32();
                    model.ChannelsOccupy = reader["ChannelsOccupy"].ToInt32();
                    model.ChannelsPresent = reader["ChannelsPresent"].ToInt32();
                    model.Active = reader["Active"].ToInt32();
                    list.Add(model);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取所有仪器
        /// </summary>
        /// <returns></returns>
        private static async Task<string[]> GetInstrumentList()
        {
            using (var conn = new OleDbConnection(connection))
            {
                conn.Open();
                var sb = new StringBuilder();
                OleDbCommand cmd = new OleDbCommand("select * from Instrument", conn);
                DbDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    sb.Append(reader["InstrumentName"].ToString().Trim() + ",");
                }

                var str = sb.ToString();
                return str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public static async Task<List<ModelList>> GetModelList()
        {
            List<ModelList> list = new List<ModelList>();
            var sql = "select * from ModelList";
            using (var conn = new OleDbConnection(connection))
            {
                try
                {
                    conn.Open();
                    var cmd = new OleDbCommand(sql, conn);
                    var reader = await cmd.ExecuteReaderAsync();
                    while (reader.Read())
                    {
                        var model = new ModelList
                        {
                            InstrumentName = reader["InstrumentName"].ToString(),
                            ModelName = reader["ModelName"].ToString(),
                            InterfaceAvailable = reader["InterfaceAvailable"].ToString(),
                            ModulesMax = reader["ModulesMax"].ToInt32(),
                            ChannelsPerModule = reader["ChannelsPerModule"].ToInt32(),
                            DefaultParameter = reader["DefaultParameter"].ToString(),
                            APIDLL = reader["APIDLL"].ToString(),
                            DRVDLL = reader["DRVDLL"].ToString(),
                            SubDevices = reader["SubDevices"].ToString(),
                            ModuleClassify = reader["ModuleClassify"].ToString(),
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
    }
}
