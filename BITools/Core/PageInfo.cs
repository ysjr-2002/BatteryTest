using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.Core
{
    public class PageInfo : PropertyNotifyObject
    {
        public PageInfo()
        {
            PageIndex = 1;
            PageSize = 30;
        }

        public int PageSize
        {
            get { return this.GetValue(s => s.PageSize); }
            set { this.SetValue(s => s.PageSize, value); }
        }

        public int PageIndex
        {
            get { return this.GetValue(s => s.PageIndex); }
            set { this.SetValue(s => s.PageIndex, value); }
        }

        public int RecordTotal
        {
            get { return this.GetValue(s => s.RecordTotal); }
            set
            {
                this.SetValue(s => s.RecordTotal, value);
                PageTotal = getPageTotal();
            }
        }

        private int getPageTotal()
        {
            if (RecordTotal == 0)
                return 1;
            else
            {
                var p = RecordTotal / PageSize;
                var m = RecordTotal % PageSize;
                if (m != 0)
                    return (p + 1);
                else
                    return p;
            }
        }

        public int PageTotal
        {
            get
            {
                return this.GetValue(s => s.PageTotal);
            }
            set { this.SetValue(s => s.PageTotal, value); }
        }
    }
}
