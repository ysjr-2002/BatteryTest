using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.ViewModel
{
    /// <summary>
    /// 老化设备信息
    /// </summary>
    public class DeveInfo : PropertyNotifyObject
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string sbbh
        {
            get { return this.GetValue(c => c.sbbh); }
            set { this.SetValue(c => c.sbbh, value); }
        }

        /// <summary>
        /// 扫描
        /// </summary>
        public int sm
        {
            get { return this.GetValue(c => c.sm); }
            set { this.SetValue(c => c.sm, value); }
        }

        /// <summary>
        /// 老化参数调用
        /// </summary>
        public string lhcsdy
        {
            get { return this.GetValue(c => c.lhcsdy); }
            set { this.SetValue(c => c.lhcsdy, value); }
        }

        /// <summary>
        /// AC输入
        /// </summary>
        public string acsr
        {
            get { return this.GetValue(c => c.acsr); }
            set { this.SetValue(c => c.acsr, value); }
        }

        /// <summary>
        /// AC状态
        /// </summary>
        public string aczt
        {
            get { return this.GetValue(c => c.aczt); }
            set { this.SetValue(c => c.aczt, value); }
        }

        /// <summary>
        /// 总数
        /// </summary>
        public int zs
        {
            get { return this.GetValue(c => c.zs); }
            set { this.SetValue(c => c.zs, value); }
        }

        /// <summary>
        /// 合格
        /// </summary>
        public int hg
        {
            get { return this.GetValue(c => c.hg); }
            set { this.SetValue(c => c.hg, value); }
        }

        /// <summary>
        /// 不良
        /// </summary>
        public int bl
        {
            get { return this.GetValue(c => c.bl); }
            set { this.SetValue(c => c.bl, value); }
        }

        /// <summary>
        /// 不良率
        /// </summary>
        public string bll
        {
            get { return this.GetValue(c => c.bll); }
            set { this.SetValue(c => c.bll, value); }
        }

        /// <summary>
        /// 产品型号
        /// </summary>
        public string cpxh
        {
            get { return this.GetValue(c => c.cpxh); }
            set { this.SetValue(c => c.cpxh, value); }
        }

        /// <summary>
        /// 产品订单号
        /// </summary>
        public string cpddh
        {
            get { return this.GetValue(c => c.cpddh); }
            set { this.SetValue(c => c.cpddh, value); }
        }
    }
}
