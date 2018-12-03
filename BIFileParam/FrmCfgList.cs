using BIModel.Access;
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
    public partial class FrmCfgList : FixForm
    {
        public FrmCfgList()
        {
            InitializeComponent();
        }

        private async void FrmCfgList_Load(object sender, EventArgs e)
        {
            dgFiles.AutoGenerateColumns = false;
            dgFiles.MultiSelect = false;
            dgFiles.DataSource = await AccessDBHelper.CfgList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgFiles.SelectedRows == null)
                return;

            var model = dgFiles.SelectedRows[0].DataBoundItem as HWCfgFileModel;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
