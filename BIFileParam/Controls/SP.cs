using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIFileParam.Controls
{
    public partial class SP : Label
    {
        public SP()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.Width = 200;
            this.Height = 2;
            this.BorderStyle = BorderStyle.Fixed3D;
        }

        public SP(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}
