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
                new DataColumn("SvrID",typeof(string)),
                new DataColumn("SuperID",typeof(string)),
                new DataColumn("GroupID",typeof(string)),
                new DataColumn("FullPath",typeof(string)),
                new DataColumn("Extension",typeof(string)),
                new DataColumn("Cover",typeof(string)),
                new DataColumn("Thumbnail",typeof(byte[])),
                new DataColumn("Timing",typeof(string)),
                new DataColumn("SourceDesc",typeof(string)),
                new DataColumn("Lat",typeof(double)),
                new DataColumn("Lon",typeof(double)),
                new DataColumn("MapKind",typeof(string)),
                new DataColumn("Width",typeof(string)),
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
