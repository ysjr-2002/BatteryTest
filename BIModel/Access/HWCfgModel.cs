using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel.Access
{
    public class HWCfgModel : BaseM
    {
        public HWCfgModel()
        {
            ModelIndex = 1;
            ModulesMax = 1;
            ChannelsPerModule = 1;
            SubDevices = "";
            ModuleClassify = "";
            Active = 0;
        }

        public string Interface { get; set; }
        public string InterfaceParameter { get; set; }
        public int ModulesMax { get; set; }
        public int ChannelsPerModule { get; set; }
        public string SubDevices { get; set; }
        public string ModuleClassify { get; set; }
    }
}
