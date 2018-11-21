using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    class TestRun
    {
        private DataTable tabFileData;
        private SqlInfo fileSqlInfo;

        public void Test()
        {
            tabFileData = new System.Data.DataTable();
            tabFileData.TableName = "FileData";
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
                new DataColumn("Width",typeof(string)), //
                new DataColumn("Height",typeof(string)),
                new DataColumn("SortIndex",typeof(int)),
                new DataColumn("Factory",typeof(string)),
                new DataColumn("PlayTime",typeof(string)),
                new DataColumn("OriPath",typeof(string)),
                new DataColumn("OdPath",typeof(string)),
                new DataColumn("OiPath",typeof(string)),
                new DataColumn("BgPath",typeof(string)),
                new DataColumn("FrameRate",typeof(string))
            });
            tabFileData.PrimaryKey = new DataColumn[] { tabFileData.Columns["ID"] };
            fileSqlInfo = new SqlInfo(tabFileData, SqliteHelper.Instance);
        }
    }
}
