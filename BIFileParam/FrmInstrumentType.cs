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
    public partial class FrmInstrumentType : Form
    {
        public FrmInstrumentType()
        {
            InitializeComponent();
        }

        private async void FrmDeviceType_Load(object sender, EventArgs e)
        {
            var list = await AccessDBHelper.InstrumentList();
            foreach (var item in list)
            {
                lbInstrumentType.Items.Add(item.InstrumentName);
            }
        }

        public string InstrumentName { get; set; }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InstrumentName = lbInstrumentType.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void lbInstrumentType_DoubleClick(object sender, EventArgs e)
        {
            InstrumentName = lbInstrumentType.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
