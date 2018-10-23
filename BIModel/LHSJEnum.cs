using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel
{
    /// <summary>
    /// 老化时间枚举
    /// </summary>
    public enum LHSJEnum
    {
        /// <summary>
        /// 老化总时间
        /// </summary>
        [Description("老化总时间")]
        LHZSJ,
        /// <summary>
        /// 开始时间
        /// </summary>
        [Description("开始时间")]
        KSSJ,
        /// <summary>
        /// 结束时间
        /// </summary>
        [Description("结束时间")]
        JSSJ
    }
}
