using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.Enums
{
    /// <summary>
    /// 负载通道输出类型
    /// </summary>
    enum OutputEnum : int
    {
        /// <summary>
        /// 电压
        /// </summary>
        V,
        /// <summary>
        /// 电流
        /// </summary>
        A,
        /// <summary>
        /// 温度
        /// </summary>
        T,
        /// <summary>
        /// 未知
        /// </summary>
        N
    }
}
