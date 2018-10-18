using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel
{
    /// <summary>
    /// 老化数据记录
    /// </summary>
    public class DataRecord
    {
        public string Time { get; set; }

        public string Input { get; set; }

        public string X { get; set; }

        public string Voltage { get; set; }

        public string Ammeter { get; set; }
    }
}
