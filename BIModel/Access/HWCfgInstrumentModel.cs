using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel.Access
{
    /// <summary>
    /// 
    /// </summary>
    public class HWCfgInstrumentModel
    {
        public HWCfgInstrumentModel()
        {
            this.Active = 1;
        }

        public int ID { get; set; }

        public string InstrumentName { get; set; }

        public string HWName { get; set; }

        public int Active { get; set; }
    }
}
