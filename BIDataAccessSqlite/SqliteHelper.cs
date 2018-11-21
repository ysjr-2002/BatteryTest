using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    public class SqliteHelper
    {
        private static object lckSyn = new object();

        private SqliteHelper()
        {
        }

        private static SqliteHelper helper = null;
        public static SqliteHelper Instance
        {
            get
            {
                if (helper == null)
                {
                    lock (lckSyn)
                    {
                        if (helper == null)
                            helper = new SqliteHelper();
                    }
                }

                return helper;
            }
        }

        private SQLiteDatabase _db;

        public bool IsInited
        {
            get;
            private set;
        }

        public bool IsFirstRun
        {
            get;
            private set;
        }

        public bool Init(string dbPath)
        {
            try
            {
                if (_db == null)
                {
                    if (System.IO.File.Exists(dbPath))
                        IsFirstRun = false;
                    else
                        IsFirstRun = true;

                    _db = new SQLiteDatabase(dbPath, false);
                }
                IsInited = true;
                return true;
            }
            catch (Exception ex)
            {
                this.WriteExcaption("Init", ex);
                return false;
            }
        }

        public bool ExecNoQuery(string sql, System.Data.Common.DbParameter[] dbParams)
        {
            var ret = this._db.ExecuteNonQuery(sql, dbParams);
            return ret > 0;
        }

        public string ExecScalar(string sql)
        {
            return _db.ExecuteScalar(sql);
        }

        public bool SaveBatch(string sql)
        {
            return _db.SaveBatch(sql);
        }

        public System.Data.DataTable GetDataTable(string sql)
        {
            return _db.GetDataTable(sql);
        }

        public bool CreateTab(System.Data.DataTable tab, bool isPrimaryKeyAutoIncrement = false)
        {
            try
            {
                if (!this.IsInited)
                    throw new Exception("local data init failed..");
                _db.UpdateTable(tab, isPrimaryKeyAutoIncrement);
                return true;
            }
            catch (Exception ex)
            {
                this.WriteExcaption("CreateTab", ex);
                return false;
            }
        }

        private void WriteExcaption(string funName, Exception ex)
        {

        }

        internal System.Data.Common.DbParameter CreateInDbParam(string pName, System.Data.DbType dbType, object value = null)
        {
            return _db.CreateInDbParameter(pName, dbType, value);
        }

        internal System.Data.Common.DbParameter CreateInDbParam(string pName, Type colType, object value = null)
        {
            System.Data.DbType dbType = ToDbType(colType);
            return _db.CreateInDbParameter(pName, dbType, value);
        }

        public bool TabIsExits(string tabName)
        {
            if (_db == null) return false;
            return _db.TableIsExist(tabName);
        }

        private System.Data.DbType ToDbType(Type type)
        {
            switch (type.Name.ToLower())
            {
                case "byte[]":
                    return System.Data.DbType.Binary;
                case "int32":
                case "single":
                    return System.Data.DbType.Int32;
                case "bool":
                    return System.Data.DbType.Boolean;
                case "datetime":
                    return System.Data.DbType.DateTime;
                case "double":
                    return System.Data.DbType.Double;
                case "string":
                default:
                    return System.Data.DbType.String;
            }
        }
    }
}
