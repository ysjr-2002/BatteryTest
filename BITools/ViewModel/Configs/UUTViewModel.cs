using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows;
using BITools.Model;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using BIDataAccess;
using BIData;
using BICommon.Enums;

namespace BITools.ViewModel.Configs
{
    /// <summary>
    /// 台车
    /// </summary>
    public class UUTViewModel : PropertyNotifyObject
    {
        public UUTViewModel()
        {
            this.ChannelList = new ObservableCollection<ChannelViewModel>();
            this.StateBrush = "/BITools;component/Images/Status/tj.png";
            this.IsEnable = true;
        }

        public string Code
        {
            get { return this.GetValue(c => c.Code); }
            set { this.SetValue(c => c.Code, value); }
        }

        public bool IsEnable
        {
            get { return this.GetValue(c => c.IsEnable); }
            set { this.SetValue(c => c.IsEnable, value); }
        }

        [JsonIgnore]
        public string StateBrush
        {
            get { return this.GetValue(c => c.StateBrush); }
            set { this.SetValue(c => c.StateBrush, value); }
        }

        /// <summary>
        /// 通道集合
        /// </summary>
        public ObservableCollection<ChannelViewModel> ChannelList
        {
            get { return this.GetValue(c => c.ChannelList); }
            set { this.SetValue(c => c.ChannelList, value); }
        }

        public void ParseData(string tc, string layer, int lhsj, string data, bool isSavedata)
        {
            var fzcount = this.ChannelList.Count(c => c.ChannelType == (int)ChannelTypeEnum.FZ);
            var items = data.Split(';');
            if (items.Length != fzcount)
                return;

            //负载通道1
            var c1_sxv = ChannelList[0].MontiorParamList[3].Val.ToFloat();
            var c1_sxa = ChannelList[0].MontiorParamList[5].Val.ToFloat();

            var c1_xxv = ChannelList[0].MontiorParamList[4].Val.ToFloat();
            var c1_xxa = ChannelList[0].MontiorParamList[6].Val.ToFloat();

            //负载通道2
            var c2_sxv = ChannelList[1].MontiorParamList[3].Val.ToFloat();
            var c2_sxa = ChannelList[1].MontiorParamList[5].Val.ToFloat();

            var c2_xxv = ChannelList[1].MontiorParamList[4].Val.ToFloat();
            var c2_xxa = ChannelList[1].MontiorParamList[6].Val.ToFloat();


            var fz1 = items[0].Split(',');
            var c1_v = fz1[0].ToFloat();
            var c1_a = fz1[1].ToFloat();

            var fz2 = items[1].Split(',');
            var c2_v = fz2[0].ToFloat();
            var c2_a = fz2[1].ToFloat();

            if (c1_v <= c1_sxv) //欠压->电压读取值小于上限
            {
                ChangeState(UUTStateEnum.qy);
            }
            if (c1_a <= c1_sxa) //欠流->电压读取值小于上限
            {

                ChangeState(UUTStateEnum.ql);
            }


            if (isSavedata)
            {
                OrderData orderdata = new OrderData();
                orderdata.TC = tc;
                orderdata.Layer = layer;
                orderdata.UUTCode = this.Code;
                orderdata.Time = FunExt.IntToTimeSpan(lhsj);
                orderdata.Content = data;
                DataOperator.Instance.SaveOrderData(orderdata);
            }
        }

        private void ChangeState(UUTStateEnum state)
        {
            var index = (byte)state;
            if (index == 0)
                StateBrush = "/BITools;component/Images/Status/tj.png";
            else if (index == 1)
                StateBrush = "/BITools;component/Images/Status/lzyc.png";
            else if (index == 2)
                StateBrush = "/BITools;component/Images/Status/wcp.png";
            else if (index == 3)
                StateBrush = "/BITools;component/Images/Status/qy.png";
            else if (index == 4)
                StateBrush = "/BITools;component/Images/Status/ql.png";
            else if (index == 5)
                StateBrush = "/BITools;component/Images/Status/gy.png";
            else if (index == 6)
                StateBrush = "/BITools;component/Images/Status/gl.png";
            else if (index == 7)
                StateBrush = "/BITools;component/Images/Status/wsc.png";
            else if (index == 8)
                StateBrush = "/BITools;component/Images/Status/hg.png";
            else if (index == 9)
                StateBrush = "/BITools;component/Images/Status/fzbh.png";
        }

        public void Init()
        {
            var c1 = new ChannelViewModel { Code = "1", Name = "负载通道1", ChannelType = (int)ChannelTypeEnum.FZ };
            var c2 = new ChannelViewModel { Code = "2", Name = "负载通道2", ChannelType = (int)ChannelTypeEnum.FZ };
            var c3 = new ChannelViewModel { Code = "3", Name = "机台通道", ChannelType = (int)ChannelTypeEnum.JT };
            var c4 = new ChannelViewModel { Code = "4", Name = "温度通道", ChannelType = (int)ChannelTypeEnum.Temperature };
            c1.Init();
            c2.Init();
            ChannelList.Add(c1);
            ChannelList.Add(c2);
            ChannelList.Add(c3);
            ChannelList.Add(c4);
        }
    }
}
