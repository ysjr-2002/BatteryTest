using BIData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDataAccess
{
    public class DataOperator
    {
        private static DataOperator _instance = new DataOperator();
        private DataTable tabOrder;
        private SqlInfo orderSqlInfo;

        private DataTable tabOrderData;
        private SqlInfo orderDataSqlInfo;

        public static DataOperator Instance
        {
            get { return _instance; }
        }

        public void CreateOrderTable()
        {
            tabOrder = new DataTable();
            tabOrder.TableName = "CPOrder";
            tabOrder.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID",typeof(int)),
                new DataColumn("CPXH",typeof(string)),         //产品型号
                new DataColumn("DDH",typeof(string)),          //订单号
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

            SqliteHelper.Instance.CreateTab(tabOrder, true);
        }

        public void CreateOrderDataTable()
        {
            tabOrderData = new DataTable();
            tabOrderData.TableName = "OrderData";
            tabOrderData.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID",typeof(int)),
                new DataColumn("OrderID",typeof(string)), //订单ID
                new DataColumn("TC",typeof(string)),      //台车
                new DataColumn("Layer",typeof(string)),   //层
                new DataColumn("UUTCode",typeof(string)), //机台号
                new DataColumn("Time",typeof(string)),    //读取时间 00:00:01
                new DataColumn("Content",typeof(string)), //读取值
            });

            tabOrderData.PrimaryKey = new DataColumn[] { tabOrderData.Columns["ID"] };
            orderDataSqlInfo = new SqlInfo(tabOrderData, SqliteHelper.Instance);

            SqliteHelper.Instance.CreateTab(tabOrderData, true);
        }

        public bool SaveOrder(Order order)
        {
            try
            {
                SetOrderValue(order);
                string strSql = string.Empty;
                if (orderSqlInfo.IsExistsPrimary(order.ID))
                {
                    strSql = orderSqlInfo.StrUpdateSql.ToString();
                }
                else
                {
                    strSql = orderSqlInfo.StrInsertSql.ToString();
                }
                return SqliteHelper.Instance.ExecNoQuery(strSql, orderSqlInfo.SqlParams);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SaveOrderData(OrderData data)
        {
            try
            {
                SetOrderDataValue(data);
                string strSql = string.Empty;
                if (orderDataSqlInfo.IsExistsPrimary(data.ID))
                {
                    strSql = orderDataSqlInfo.StrUpdateSql.ToString();
                }
                else
                {
                    strSql = orderDataSqlInfo.StrInsertSql.ToString();
                }
                return SqliteHelper.Instance.ExecNoQuery(strSql, orderDataSqlInfo.SqlParams);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Query()
        {
            var name = "";
            System.Data.DataTable tab = orderDataSqlInfo.GetTab(string.Format(" AND Name = '{0}'", name));
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

        private void SetOrderValue(Order order)
        {
            //参数名必须和列大、小写一致
            orderSqlInfo.ResetDbParamValue();
            orderSqlInfo.SetDbParamValue("ID", order.ID);
            orderSqlInfo.SetDbParamValue("CPXH", order.cpxh);
            orderSqlInfo.SetDbParamValue("DDH", order.ddh);
            orderSqlInfo.SetDbParamValue("ACInput", order.acinput);
            orderSqlInfo.SetDbParamValue("PFileName", order.pfilename);
            orderSqlInfo.SetDbParamValue("YJPZCSM", order.YJPZCSM);
            orderSqlInfo.SetDbParamValue("User", order.user);
            orderSqlInfo.SetDbParamValue("ParamContent", order.paramcontent);
            orderSqlInfo.SetDbParamValue("StartTime", order.starttime);
            orderSqlInfo.SetDbParamValue("EndTime", order.endtime);
        }

        private void SetOrderDataValue(OrderData file)
        {
            orderDataSqlInfo.ResetDbParamValue();
            orderDataSqlInfo.SetDbParamValue("ID", file.OrderID);
            orderDataSqlInfo.SetDbParamValue("OrderID", file.OrderID);
            orderDataSqlInfo.SetDbParamValue("TC", file.TC);
            orderDataSqlInfo.SetDbParamValue("Layer", file.Layer);
            orderDataSqlInfo.SetDbParamValue("UUTCode", file.UUTCode);
            orderDataSqlInfo.SetDbParamValue("Time", file.Time);
            orderDataSqlInfo.SetDbParamValue("Content", file.Content);
        }
    }
}
