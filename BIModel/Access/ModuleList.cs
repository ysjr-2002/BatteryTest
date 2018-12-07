using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel.Access
{
    public class ModuleList
    {
        public string InstrumentName { get; set; }
        public string ModuleClassify { get; set; }
        public string ModuleName { get; set; }
        public int ModulesOccupy { get; set; }
        public int ChannelsOccupy { get; set; }
        public int ChannelsPresent { get; set; }
    }
}
