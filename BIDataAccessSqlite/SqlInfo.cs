using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    class SqlInfo
    {
        private SqliteHelper dbInstance;
        private System.Data.DataTable tab;
        private Dictionary<string, System.Data.Common.DbParameter> dicList;

        public SqlInfo(System.Data.DataTable tab, SqliteHelper instance)
        {
            dbInstance = instance;
            StrUpdateSql = new StringBuilder();
            StrInsertSql = new StringBuilder();
            dicList = new Dictionary<string, System.Data.Common.DbParameter>();
            SetTable(tab);
        }

        public bool SetTable(System.Data.DataTable tabInfo)
        {
            if (tabInfo.PrimaryKey.Length == 0) return false;

            dicList.Clear();
            foreach (System.Data.DataColumn col in tabInfo.Columns)
            {
                System.Data.Common.DbParameter param =
                    this.dbInstance.CreateInDbParam(string.Format("@{0}", col.ColumnName), col.DataType);
                dicList.Add(col.ColumnName, param);
            }

            tab = tabInfo;
            InitInsertSql();
            InitUpdateSql();
            return true;
        }

        internal System.Data.DataTable Tab
        {
            get { return this.tab; }
        }

        internal string PrimaryKey
        {
            get
            {
                return this.tab.PrimaryKey[0].ColumnName;
            }
        }

        private void InitInsertSql()
        {
            string sCol = string.Empty;
            string sParam = string.Empty;
            int iIndex = 0;
            string split = "";//分隔符
            string nLine = "";//换行符

            foreach (string key in dicList.Keys)
            {
                if (PrimaryKey == key)
                    continue;

                split = iIndex == dicList.Keys.Count - 2 ? "" : ",";
                //nLine = iIndex > 0 && iIndex % 5 == 0 ? Environment.NewLine : "";

                sCol += key + split + nLine;
                sParam += dicList[key].ParameterName + split + nLine;
                iIndex++;
            }

            StrInsertSql.Clear();
            StrInsertSql.Append(string.Format("INSERT INTO {0}(", tab.TableName));
            StrInsertSql.Append(sCol);
            StrInsertSql.Append(") VALUES(");
            StrInsertSql.Append(sParam);
            StrInsertSql.Append(")");
        }

        private void InitUpdateSql()
        {
            System.Data.DataColumn col = tab.PrimaryKey[0];
            StrUpdateSql.Clear();
            StrUpdateSql.AppendLine(string.Format("UPDATE {0} SET ", tab.TableName));
            string splite = ",";
            int index = 0;
            foreach (var key in dicList.Keys)
            {
                if (key.Equals(col.ColumnName))
                {
                    index++;
                    continue;
                }

                splite = index == dicList.Keys.Count - 1 ? "" : ",";
                StrUpdateSql.AppendLine(string.Format("{0}={1}{2}", key, dicList[key].ParameterName, splite));
                index++;
            }
            StrUpdateSql.AppendLine(string.Format(" WHERE {0}={1}", col.ColumnName, dicList[col.ColumnName].ParameterName));
        }

        public bool SetDbParamValue(string colName, object value)
        {
            if (!dicList.ContainsKey(colName)) return false;
            dicList[colName].Value = value;
            return true;
        }

        public void ResetDbParamValue()
        {
            foreach (var item in dicList.Keys)
            {
                dicList[item].Value = null;
            }
        }

        public string SelectString
        {
            get
            {
                return string.Format("SELECT * FROM {0} WHERE 1 = 1 ", tab.TableName);
            }
        }

        public string DeleteString
        {
            get
            {
                return string.Format("DELETE FROM {0} WHERE 1 = 1", tab.TableName);
            }
        }

        public StringBuilder StrUpdateSql { get; private set; }

        public StringBuilder StrInsertSql { get; private set; }

        public System.Data.Common.DbParameter[] SqlParams
        {
            get
            {
                return dicList.Values.ToArray<System.Data.Common.DbParameter>();
            }
        }

        public string GetPrimaryKeyValue(string filter)
        {
            if (string.IsNullOrEmpty(filter)) return string.Empty;
            string strSql = string.Format("SELECT {0} FROM {1} WHERE 1 = 1 {2} LIMIT 0,1", PrimaryKey, tab.TableName, filter);
            return this.dbInstance.ExecScalar(strSql);
        }

        public bool IsExistsPrimary(int value)
        {
            string primaryKey = tab.PrimaryKey[0].ColumnName;
            string strSql = string.Format("SELECT 1 FROM {0} WHERE {1} = '{2}'", tab.TableName, primaryKey, value);
            return !string.IsNullOrEmpty(this.dbInstance.ExecScalar(strSql));
        }

        public System.Data.DataTable GetTab(string strFilter = "")
        {
            string strSql = !string.IsNullOrEmpty(strFilter) ?
                string.Format("{0} {1}", SelectString, strFilter) :
                SelectString;
            return this.dbInstance.GetDataTable(strSql);
        }

        public System.Data.DataTable GetTabPager(int index, int size, ref int total, bool isDes = false, string strFilter = "", string sortField = "ID")
        {
            string strStrWhere = string.Empty;
            if (!string.IsNullOrEmpty(strFilter))
            {
                strStrWhere = string.Format(" AND {0} ", strFilter);
            }

            string strSort = string.Empty;
            if (!string.IsNullOrEmpty(sortField))
            {
                strSort = string.Format(" ORDER BY {0} {1} ", sortField, (isDes ? " DES " : " AES "));
            }

            string strCountSql = string.Format("SELECT COUNT(*) FROM {0} WHERE 1=1 {1} ",
                tab.TableName, strStrWhere);

            var data = this.dbInstance.ExecScalar(strCountSql);
            if (data != null)
            {
                int itotal = 0;
                if (int.TryParse(total.ToString(), out itotal))
                {
                    total = itotal;
                }
            }

            string strSql = string.Format("SELECT * FROM {0} WHERE 1=1 {1} {2} LIMIT {3} OFFSET {3}*{4}",
                tab.TableName, strStrWhere, strSort, size, index - 1);

            return this.dbInstance.GetDataTable(strSql);
        }

        public System.Data.DataRow GetDataByPrimarykey(int value)
        {
            if (!IsExistsPrimary(value)) return null;
            System.Data.DataTable tab = this.GetTab(string.Format(" AND {0} = '{1}'", PrimaryKey, value));
            if (tab == null || tab.Rows.Count == 0) return null;

            return tab.Rows[0];
        }

        public string GetFilter(string[] keys, string field)
        {
            if (keys == null || keys.Length == 0) return string.Empty;
            string filter = string.Empty;
            for (int i = 0; i < keys.Length; i++)
            {
                string key = keys[i];
                filter += string.Format(" {0} LIKE '{0}%' {2}",
                    field, key.Replace("'", ""), i == keys.Length - 1 ? "" : "OR");
            }

            return string.Format(" ({0}) ", filter);
        }

        /// <summary>
        /// 清空表记录
        /// </summary>
        /// <returns></returns>
        public bool RemoveAllData()
        {
            if (!this.dbInstance.TabIsExits(this.Tab.TableName)) return false;
            string strSql = string.Format("DELETE FROM {0}", tab.TableName);
            return this.dbInstance.ExecNoQuery(strSql, null);
        }

        public bool DeletePrimary(string value)
        {
            string key = tab.PrimaryKey[0].ColumnName;
            string strSql = string.Format("DELETE FROM {0} WHERE {1}='{2}'", tab.TableName, key, value);
            return this.dbInstance.ExecNoQuery(strSql, null);
        }

        public int GetCount(string filter)
        {
            string strSql = string.Format("SELECT COUNT(*) FROM {0} WHERE {1}", tab.TableName, filter);
            var count = this.dbInstance.ExecScalar(strSql);
            return Convert.ToInt32(count);
        }

        public bool DeleteByCondition(string condition)
        {
            string strSql = string.Format("DELETE FROM {0} WHERE {1}", tab.TableName, condition);
            return this.dbInstance.ExecNoQuery(strSql, null);
        }

        public bool ExecUpdate()
        {
            return this.dbInstance.ExecNoQuery(this.StrUpdateSql.ToString(), this.SqlParams);
        }

        public bool ExecInsert()
        {
            return this.dbInstance.ExecNoQuery(this.StrInsertSql.ToString(), this.SqlParams);
        }

        public bool ExecDelete(string filter)
        {
            return this.dbInstance.ExecNoQuery(string.Format("{0} {1}", DeleteString, filter), null);
        }
    }
}
