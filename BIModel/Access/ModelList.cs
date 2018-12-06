using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel.Access
{
    public class ModelList
    {
        public string InstrumentName { get; set; }
        public string ModelName { get; set; }
        public string InterfaceAvailable { get; set; }
        public int ModulesMax { get; set; }
        public int ChannelsPerModule { get; set; }
        public string DefaultParameter { get; set; }
        public string APIDLL { get; set; }
        public string DRVDLL { get; set; }
        public string SubDevices { get; set; }
        public string ModuleClassify { get; set; }

        public override string ToString()
        {
            return this.ModelName;
        }
    }
}
