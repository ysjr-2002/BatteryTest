using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    public class TestRun
    {
        private DataTable tabFileData;
        private SqlInfo fileSqlInfo;

        public void CreateTable()
        {
            tabFileData = new System.Data.DataTable();
            tabFileData.TableName = "CollectData";
            tabFileData.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID",typeof(string)),
                new DataColumn("TC",typeof(string)),
                new DataColumn("Layer",typeof(string)),
                new DataColumn("CPXH",typeof(string)),
                new DataColumn("DHK",typeof(string)),
                new DataColumn("ACInput",typeof(string)), //老化电压
                new DataColumn("CSM",typeof(string)), //参数名
                new DataColumn("YJPZCSM",typeof(byte[])),//硬件配置参数名
                new DataColumn("User",typeof(string)), //用户
                new DataColumn("SourceDesc",typeof(string)), //参数值
                new DataColumn("Lat",typeof(double)), //老化开始时间
                new DataColumn("Lon",typeof(double)), //老化结束时间
                new DataColumn("MapKind",typeof(string)), //读取时间
                new DataColumn("Content",typeof(string)), //
            });
            tabFileData.PrimaryKey = new DataColumn[] { tabFileData.Columns["ID"] };
            fileSqlInfo = new SqlInfo(tabFileData, SqliteHelper.Instance);

            SqliteHelper.Instance.CreateTab(tabFileData);
        }

        public void Query()
        {
            var name = "";
            System.Data.DataTable tab = fileSqlInfo.GetTab(string.Format(" AND Name = '{0}'", name));
            if (tab != null && tab.Rows.Count > 0)
            {
                this.InitGroupByRow(tab.Rows[0]);
            }
        }


        private void InitGroupByRow(System.Data.DataRow dRow)
        {
            //string id = dRow.GetRowDefValue("ID");
            //string name = dRow.GetRowDefValue("Name");
            //IFileGroup group = new FileGroup(name, id);
            //string dtTime = dRow.GetRowDefValue("CreateTime", DateTime.MinValue.ToString());
            //group.BuilderTime = dtTime.ToDateTime();
            //return group;
        }
    }
}
