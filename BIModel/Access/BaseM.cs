using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIModel.Access
{
    public class BaseM
    {
        public string ModelName { get; set; }
        public string HWName { get; set; }
        public string InstrumentName { get; set; }
        public int ModelIndex { get; set; }
        public int Active { get; set; }
    }
}
