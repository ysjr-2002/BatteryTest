using BIData;
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

        public List<Order> QueryOrder(string condition, QueryPageInfo page)
        {
            var sql = "SELECT * FROM player where 1=1 {0} order by checktime ";
            var sqlCount = "SELECT COUNT(*) FROM player where 1=1 {0}";
            var sqlSplit = "limit {0} offset {0}*{1}";
            sql = string.Format(sql, condition);
            sqlCount = string.Format(sqlCount, condition);
            page.RecordTotal = Convert.ToInt32(_db.ExecuteScalar(sqlCount));

            sqlSplit = string.Format(sqlSplit, page.PageSize, page.PageIndex - 1);

            sql += sqlSplit;

            List<Order> list = new List<Order>();
            using (var rdr = _db.ExecuteReader(sql))
            {
                if (rdr == null)
                    return list;

                while (rdr.Read())
                {
                    var runner = ReaderToRunner(rdr);
                    list.Add(runner);
                }
            }
            return list;
        }

        private Order ReaderToRunner(IDataReader rdr)
        {
            return new Order();
            //var col = rdr["CheckTime"].ToString();
            //DateTime? checkTime;
            //if (col.IsEmpty())
            //    checkTime = null;
            //else
            //    checkTime = col.ToDateTime();

            //Order r = new Order
            //{
            //    Id = rdr["ID"].ToString(),
            //    No = rdr["NO"].ToString(),
            //    ChipCode = rdr["ChipCode"].ToString(),
            //    Bib = rdr["BIB"].ToString(),
            //    Name = rdr["Name"].ToString(),
            //    Gender = rdr["Gender"].ToString(),
            //    Photo = rdr["Photo"].ToString(),
            //    Snap = rdr["Snap"].ToString(),
            //    CheckTime = checkTime,
            //    IsCheck = rdr["IsCheck"].ToInt32() == 1,
            //    IsAlarm = rdr["Alarm"].ToInt32() == 1,
            //    Group = rdr["IGroup"].ToString(),
            //    BBCode = rdr["BBCode"].ToString(),
            //    CardID = rdr["CardId"].ToString()
            //};
            //return r;
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
            DbType dbType = ToDbType(colType);
            return _db.CreateInDbParameter(pName, dbType, value);
        }

        public bool TabIsExits(string tabName)
        {
            if (_db == null) return false;
            return _db.TableIsExist(tabName);
        }

        private DbType ToDbType(Type type)
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
