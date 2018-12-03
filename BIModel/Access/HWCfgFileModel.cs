using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel.Access
{
    /// <summary>
    /// 文件实体
    /// </summary>
    public class HWCfgFileModel
    {
        public string HWName { get; set; }

        public DateTime HWTime { get; set; }

        public string Author { get; set; }

        public bool SystemDefault { get; set; }
    }
}
