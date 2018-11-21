using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    class SQLiteDatabase
    {
        String dbConnection;

        /// <summary>
        ///     Default Constructor for SQLiteDatabase Class.
        /// </summary>
        public SQLiteDatabase()
        {
            dbConnection = "Data Source=recipes.s3db";
        }

        /// <summary>
        ///     Single Param Constructor for specifying the DB file.
        /// </summary>
        /// <param name="inputFile">The File containing the DB</param>
        public SQLiteDatabase(String inputFile)
            : this(inputFile, true)
        {

        }

        public SQLiteDatabase(string inputFile, bool IsCreateDef)
        {
            if (CreateDbFile(inputFile))
            {
                dbConnection = String.Format("Data Source={0}", inputFile);
                if (IsCreateDef)
                    CreateDefTab();
            }
            else if (System.IO.File.Exists(inputFile))
            {
                //同步数据表结构
                dbConnection = String.Format("Data Source={0}", inputFile);
                CreateDefTab();
            }
        }

        public void UpdateTable(DataTable dt, bool primaryKeyAutoIncrement = false)
        {
            if (string.IsNullOrEmpty(dt.TableName)) return;
            try
            {
                string autoKey = string.Empty;
                if (primaryKeyAutoIncrement && dt.PrimaryKey.Length == 1)
                {
                    autoKey = dt.PrimaryKey[0].ColumnName;
                }

                StringBuilder strSql = new StringBuilder();
                if (!TableIsExist(dt.TableName))
                {
                    strSql.AppendLine(string.Format("CREATE TABLE {0}(", dt.TableName));
                    foreach (DataColumn item in dt.Columns)
                    {
                        if (!string.IsNullOrEmpty(autoKey) && item.ColumnName.Equals(autoKey))
                        {
                            strSql.AppendLine(string.Format("[{0}] INTEGER PRIMARY KEY,", item.ColumnName));
                        }
                        else
                        {
                            strSql.AppendLine(string.Format("[{0}] {1},", item.ColumnName, this.GetDbColType(item.DataType)));
                        }
                    }

                    strSql.AppendLine(")");
                    if (strSql.Length > 0)
                        this.ExecuteNonQuery(strSql.ToString().Remove(strSql.ToString().LastIndexOf(","), 1));
                }
                else //更新表结构
                {
                    DataTable dtColums = this.GetDataTable(string.Format("PRAGMA table_info('{0}')", dt.TableName));

                    foreach (DataColumn dColum in dt.Columns)
                    {
                        DataRow[] dRow = dtColums.Select(string.Format("name='{0}'", dColum.ColumnName));
                        if (dRow != null && dRow.Length >= 1) continue;
                        string sType = this.GetDbColType(dColum.DataType);
                        if (string.IsNullOrEmpty(sType)) sType = "nvarchar";

                        this.ExecuteNonQuery(string.Format("ALTER TABLE {0} ADD COLUMN [{1}] {2}",
                            dt.TableName, dColum.ColumnName, sType));

                        if (dColum.DefaultValue != null && !string.IsNullOrEmpty(dColum.DefaultValue.ToString()))
                        {
                            this.ExecuteNonQuery(string.Format("UPDATE {0} SET {1}='{2}'",
                                dt.TableName,
                                dColum.ColumnName,
                                dColum.DefaultValue.ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //SysLogManagement.LogManagement.Logger.WriteError("UpdateTable", ex);
                throw ex;
            }
        }

        private string GetDbColType(Type type)
        {
            string sType = string.Empty;
            Console.WriteLine(type.Name);

            switch (type.Name.ToLower())
            {
                case "byte[]":
                    sType = "blob";
                    break;
                case "string":
                    sType = "nvarchar";
                    break;
                case "int32":
                case "single":
                    sType = "int";
                    break;
                case "bool":
                    sType = "bit";
                    break;
                case "datetime":
                    sType = "datetime";
                    break;
                case "boolean":
                    sType = "int";
                    break;
                default:
                    sType = type.Name.ToLower();
                    break;
            }
            return sType;
        }

        public void CreateDefTab()
        {
            //this.UpdateTable(this.TabCaseInfo);
            //this.UpdateTable(this.TabFileInfo);
            //this.UpdateTable(this.TabBsInfo);
            //this.UpdateTable(this.TabBsDetailInfo);
            //this.UpdateTable(this.TabCaseLink);
            //this.UpdateTable(this.TabPlaceInfo);
            //this.UpdateTable(this.TabWifiInfo);
            //this.UpdateTable(this.TabWifiDetail);
        }

        public bool TableIsExist(string tableName)
        {
            if (tableName == null)
            {
                return false;
            }
            try
            {
                string sql =
                    "SELECT count(*) as c from sqlite_master where type ='table' and name ='" + tableName.Trim() + "' ";
                string strCount = this.ExecuteScalar(sql);
                return "1".Equals(strCount);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CreateDbFile(string sdbName)
        {
            try
            {
                if (!System.IO.File.Exists(sdbName))
                {
                    SQLiteConnection.CreateFile(sdbName);
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 批量执语句
        /// </summary>
        /// <param name="strSql">保存或更新语句，以 ';'间隔</param>
        /// <returns></returns>
        public bool SaveBatch(string strSql)
        {
            SQLiteTransaction tran = null;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(this.dbConnection))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    tran = conn.BeginTransaction();
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strSql;
                    cmd.ExecuteNonQuery();
                    tran.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (tran != null) tran.Rollback();
                throw ex;
            }
        }

        public void SaveVideoInfo(string Sql, byte[] img)
        {
            try
            {
                SQLiteConnection connection = new SQLiteConnection(this.dbConnection);
                connection.Open();

                String sql2 = Sql;//"insert into image values('456',@a)"; //@a为要保存的Image byte[]，image为表名

                SQLiteCommand command3 = connection.CreateCommand();
                command3.CommandText = sql2;
                command3.Parameters.Add(new SQLiteParameter()
                {
                    DbType = System.Data.DbType.Binary,
                    Value = img,
                    ParameterName = "@a"
                });
                command3.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateConver(string Sql, byte[] img)
        {
            try
            {
                SQLiteConnection connection = new SQLiteConnection(this.dbConnection);
                connection.Open();

                String sql2 = Sql;//"insert into image values('456',@a)"; //@a为要保存的Image byte[]，image为表名

                SQLiteCommand command3 = connection.CreateCommand();
                command3.CommandText = sql2;
                command3.Parameters.Add(new SQLiteParameter()
                {
                    DbType = System.Data.DbType.Binary,
                    Value = img,
                    ParameterName = "@a"
                });
                command3.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CreateTable(string TabName, string[] fields, string imgField)
        {
            try
            {
                string scmd = "CREATE TABLE {0}(";
                foreach (string item in fields)
                {
                    scmd += item + ",";
                }
                if (!string.IsNullOrEmpty(imgField))
                {
                    scmd += string.Format("{0} BLOB", imgField);
                }
                //scmd = scmd.Remove(scmd.LastIndexOf(','), 1);
                scmd = string.Format(scmd + ")", TabName);
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = scmd;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CreateTable(string TabName, string[] fields)
        {
            try
            {
                string scmd = "CREATE TABLE {0}(";
                foreach (string item in fields)
                {
                    scmd += item + ",";
                }
                scmd = scmd.Remove(scmd.LastIndexOf(','), 1);
                scmd = string.Format(scmd + ")", TabName);
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                if (cnn.State == ConnectionState.Closed) cnn.Open();
                SQLiteCommand cmd = cnn.CreateCommand();
                cmd.CommandText = scmd;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///     Single Param Constructor for specifying advanced connection options.
        /// </summary>
        /// <param name="connectionOpts">A dictionary containing all desired options and their values</param>
        public SQLiteDatabase(Dictionary<String, String> connectionOpts)
        {
            String str = "";
            foreach (KeyValuePair<String, String> row in connectionOpts)
            {
                str += String.Format("{0}={1}; ", row.Key, row.Value);
            }
            str = str.Trim().Substring(0, str.Length - 1);
            dbConnection = str;
        }

        /// <summary>
        ///     Allows the programmer to run a query against the Database.
        /// </summary>
        /// <param name="sql">The SQL to run</param>
        /// <returns>A DataTable containing the result set.</returns>
        public DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteConnection cnn = new SQLiteConnection(dbConnection);
                cnn.Open();
                SQLiteDataAdapter sqlAdapter = new SQLiteDataAdapter();
                sqlAdapter.SelectCommand = cnn.CreateCommand();
                sqlAdapter.SelectCommand.CommandText = sql;

                sqlAdapter.Fill(dt);
                //SQLiteCommand mycommand = new SQLiteCommand(cnn);
                //mycommand.CommandText = sql;
                //SQLiteDataReader reader = mycommand.ExecuteReader();
                //dt.Load(reader);
                //reader.Close();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }

        /// <summary>
        ///     Allows the programmer to interact with the database for purposes other than a query.
        /// </summary>
        /// <param name="sql">The SQL to be run.</param>
        /// <returns>An Integer containing the number of rows updated.</returns>
        public int ExecuteNonQuery(string sql)
        {
            SQLiteConnection cnn = new SQLiteConnection(dbConnection);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            int rowsUpdated = mycommand.ExecuteNonQuery();
            cnn.Close();
            return rowsUpdated;
        }

        public int ExecuteNonQuery(string sql, DbParameter[] parameters)
        {
            SQLiteConnection cnn = new SQLiteConnection(dbConnection);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            if (parameters != null) mycommand.Parameters.AddRange(parameters);
            int rowsUpdated = mycommand.ExecuteNonQuery();
            cnn.Close();
            return rowsUpdated;
        }

        /// <summary>
        ///     Allows the programmer to retrieve single items from the DB.
        /// </summary>
        /// <param name="sql">The query to run.</param>
        /// <returns>A string.</returns>
        public string ExecuteScalar(string sql)
        {
            SQLiteConnection cnn = new SQLiteConnection(dbConnection);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            object value = mycommand.ExecuteScalar();
            cnn.Close();
            if (value != null)
            {
                return value.ToString();
            }
            return "";
        }

        /// <summary>
        ///     Allows the programmer to easily update rows in the DB.
        /// </summary>
        /// <param name="tableName">The table to update.</param>
        /// <param name="data">A dictionary containing Column names and their new values.</param>
        /// <param name="where">The where clause for the update statement.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool Update(String tableName, Dictionary<String, String> data, String where)
        {
            String vals = "";
            Boolean returnCode = true;
            if (data.Count >= 1)
            {
                foreach (KeyValuePair<String, String> val in data)
                {
                    vals += String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString());
                }
                vals = vals.Substring(0, vals.Length - 1);
            }
            try
            {
                this.ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
            }
            catch
            {
                returnCode = false;
            }
            return returnCode;
        }

        /// <summary>
        ///     Allows the programmer to easily delete rows from the DB.
        /// </summary>
        /// <param name="tableName">The table from which to delete.</param>
        /// <param name="where">The where clause for the delete.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool Delete(String tableName, String where)
        {
            Boolean returnCode = true;
            try
            {
                this.ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
            }
            catch (Exception fail)
            {
                //MessageBox.Show(fail.Message);
                returnCode = false;
                throw fail;
            }
            return returnCode;
        }

        /// <summary>
        ///     Allows the programmer to easily insert into the DB
        /// </summary>
        /// <param name="tableName">The table into which we insert the data.</param>
        /// <param name="data">A dictionary containing the column names and data for the insert.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool Insert(String tableName, Dictionary<String, String> data)
        {
            String columns = "";
            String values = "";
            Boolean returnCode = true;
            foreach (KeyValuePair<String, String> val in data)
            {
                columns += String.Format(" {0},", val.Key.ToString());
                values += String.Format(" '{0}',", val.Value);
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            try
            {
                this.ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
            }
            catch (Exception fail)
            {
                returnCode = false;
                throw fail;
            }
            return returnCode;
        }

        /// <summary>
        ///     Allows the programmer to easily delete all data from the DB.
        /// </summary>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearDB()
        {
            DataTable tables;
            try
            {
                tables = this.GetDataTable("select NAME from SQLITE_MASTER where type='table' order by NAME;");
                foreach (DataRow table in tables.Rows)
                {
                    this.ClearTable(table["NAME"].ToString());
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     Allows the user to easily clear all data from a specific table.
        /// </summary>
        /// <param name="table">The name of the table to clear.</param>
        /// <returns>A boolean true or false to signify success or failure.</returns>
        public bool ClearTable(String table)
        {
            try
            {

                this.ExecuteNonQuery(String.Format("delete from {0};", table));
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public DbParameter CreateInDbParameter(string paraName, DbType dbType, int size, object value)
        {
            return CreateDbParameter(paraName, dbType, size, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public DbParameter CreateInDbParameter(string paraName, DbType dbType, object value)
        {
            return CreateDbParameter(paraName, dbType, 0, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造DbParameter的实例
        /// </summary>
        public DbParameter CreateDbParameter(string paraName, DbType dbType, int size, object value, ParameterDirection direction)
        {
            DbParameter para;
            para = new System.Data.SQLite.SQLiteParameter();
            para.ParameterName = paraName;

            if (size != 0)
                para.Size = size;

            para.DbType = dbType;

            if (value != null)
                para.Value = value;

            para.Direction = direction;

            return para;
        }
    }
}
