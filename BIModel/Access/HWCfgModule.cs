using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel.Access
{
    public class HWCfgModule  : BaseM
    {
        public HWCfgModule()
        {

        }
       
        public string ModuleClassify { get; set; }
        public int Channel { get; set; }
        public int SpecifiedIndex { get; set; }
        public int ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public int ModulesOccupy { get; set; }
        public int ChannelsOccupy { get; set; }
        public int ChannelsPresent { get; set; }
    }
}
