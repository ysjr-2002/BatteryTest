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

            dgFiles.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgFiles.TopLeftHeaderCell.Value = "No.";
            dgFiles.TopLeftHeaderCell.ValueType = typeof(string);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dgFiles_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (e.RowIndex == -1)
            //    return;

            var model = dgFiles.Rows[e.RowIndex].DataBoundItem as HWCfgFileModel;
            this.DialogResult = DialogResult.OK;
        }

        private void dgFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex == 0)
            {
                dgFiles.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }
    }
}
