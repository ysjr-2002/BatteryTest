using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel.Access
{
    public class HWCfgModel
    {
        public string ModelName { get; set; }
        public string HWName { get; set; }
        public string InstrumentName { get; set; }
        public string ModelIndex { get; set; }
        public string Interface { get; set; }
        public string InterfaceParameter { get; set; }
        public string ModulesMax { get; set; }
        public string ChannelsPerModule { get; set; }
        public string SubDevices { get; set; }
        public string ModuleClassify { get; set; }
        public bool Active { get; set; }
    }
}
