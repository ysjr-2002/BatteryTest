using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel
{
    /// <summary>
    /// 功能模块
    /// </summary>
    public enum SystemModule : int
    {
        User = 1,
        Config = 2,
        Device = 4,
        History = 8
    }
}
