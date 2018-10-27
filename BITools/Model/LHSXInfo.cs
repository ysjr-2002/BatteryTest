using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.Model
{
    /// <summary>
    /// 老化时序
    /// </summary>
    public class LHSXInfo : PropertyNotifyObject
    {
        public LHSXInfo()
        {
            guid = Guid.NewGuid().ToString("N");
        }
        public string guid { get; set; }
        public RepeatUnitEnum dzzzTimeUnit { get; set; }
        public TimeUnitEnum gscTimeUnit { get; set; }
        public TimeUnitEnum kscTimeUnit { get; set; }

        public string srdy
        {
            get { return this.GetValue(c => c.srdy); }
            set { this.SetValue(c => c.srdy, value); }
        }

        public string fzcy
        {
            get { return this.GetValue(c => c.fzcy); }
            set { this.SetValue(c => c.fzcy, value); }
        }

        public string pdfw
        {
            get { return this.GetValue(c => c.pdfw); }
            set { this.SetValue(c => c.pdfw, value); }
        }

        /// <summary>
        /// 冲击开关
        /// </summary>
        public bool cjkt
        {
            get { return this.GetValue(c => c.cjkt); }
            set { this.SetValue(c => c.cjkt, value); }
        }

        /// <summary>
        /// 动作执行
        /// </summary>
        public string dzzx
        {
            get { return this.GetValue(c => c.dzzx); }
            set { this.SetValue(c => c.dzzx, value); }
        }

        /// <summary>
        /// 关时长
        /// </summary>
        public string gsc
        {
            get { return this.GetValue(c => c.gsc); }
            set { this.SetValue(c => c.gsc, value); }
        }

        /// <summary>
        /// 开时长
        /// </summary>
        public string ksc
        {
            get { return this.GetValue(c => c.ksc); }
            set { this.SetValue(c => c.ksc, value); }
        }
    }
}
