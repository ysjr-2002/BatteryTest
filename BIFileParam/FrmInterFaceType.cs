using BICommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIFileParam
{
    public partial class FrmInterFaceType : FixForm
    {
        public FrmInterFaceType()
        {
            InitializeComponent();
        }

        private void FrmInterFaceType_Load(object sender, EventArgs e)
        {
            foreach (var item in FunExt.GetSerialPorts())
            {
                cmbPorts.Items.Add(item);
            }
            cmbPorts.SelectedIndex = 0;

            foreach (var item in FunExt.Bauds())
            {
                cmbBaud.Items.Add(item);
            }
            cmbBaud.SelectedIndex = 0;

            cmbCheck.SelectedIndex = 0;
            cmbDataBit.SelectedIndex = 0;
            cmbStopBit.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
