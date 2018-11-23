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
        private DataTable tabOrder;
        private SqlInfo orderSqlInfo;

        private DataTable tabCollectData;
        private SqlInfo fileSqlInfo;

        public void CreateOrderTable()
        {
            tabOrder = new DataTable();
            tabOrder.TableName = "CPOrder";
            tabOrder.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("OrderID",typeof(string)),
                new DataColumn("CPXH",typeof(string)),         //产品型号
                new DataColumn("DHK",typeof(string)),          //订单号
                new DataColumn("ACInput",typeof(string)),      //老化电压
                new DataColumn("PFileName",typeof(string)),    //参数名
                new DataColumn("YJPZCSM",typeof(string)),      //硬件配置参数名
                new DataColumn("User",typeof(string)),         //用户
                new DataColumn("ParamContent",typeof(string)), //参数值
                new DataColumn("StartTime",typeof(string)),    //老化开始时间
                new DataColumn("EndTime",typeof(string))       //老化结束时间
            });
            tabOrder.PrimaryKey = new DataColumn[] { tabOrder.Columns["ID"] };
            orderSqlInfo = new SqlInfo(tabOrder, SqliteHelper.Instance);

            SqliteHelper.Instance.CreateTab(tabOrder);
        }

        public void CreateCollectionDataTable()
        {
            tabCollectData = new DataTable();
            tabCollectData.TableName = "CollectData";
            tabCollectData.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID",typeof(int)),
                new DataColumn("OrderID",typeof(string)), //订单ID
                new DataColumn("TC",typeof(string)),      //台车
                new DataColumn("Layer",typeof(string)),   //层
                new DataColumn("UUTCode",typeof(string)), //机台号
                new DataColumn("Time",typeof(string)),    //读取时间 00:00:01
                new DataColumn("Content",typeof(string)), //读取值
            });

            tabCollectData.PrimaryKey = new DataColumn[] { tabCollectData.Columns["ID"] };
            fileSqlInfo = new SqlInfo(tabCollectData, SqliteHelper.Instance);

            SqliteHelper.Instance.CreateTab(tabCollectData, true);
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
