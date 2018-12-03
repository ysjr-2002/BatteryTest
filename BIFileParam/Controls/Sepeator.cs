using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIFileParam.Controls
{
    public class Sepeator : Label
    {
        public Sepeator()
        {
            this.AutoSize = false;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.Width = 200;
            this.Height = 2;
        }
    }
}
