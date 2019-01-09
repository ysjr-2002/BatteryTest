using BICommon;
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
    /// <summary>
    /// 文件列表
    /// </summary>
    public partial class FrmCfgList : FixForm
    {
        public FrmCfgList()
        {
            InitializeComponent();
        }

        public HWCfgFileModel HWFile { get; set; }

        private async void FrmCfgList_Load(object sender, EventArgs e)
        {
            dgFiles.AutoGenerateColumns = false;
            dgFiles.MultiSelect = false;

            var fileList = await AccessDBHelper.CfgList();
            dgFiles.DataSource = fileList;
            this.txtFileName.Text = fileList.FirstOrDefault().HWName;

            dgFiles.TopLeftHeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgFiles.TopLeftHeaderCell.Value = "No.";
            dgFiles.TopLeftHeaderCell.ValueType = typeof(string);
        }

        private void dgFiles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.ColumnIndex == 0)
            {
                dgFiles.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
            }
        }

       

        private void dgFiles_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            var model = dgFiles.Rows[e.RowIndex].DataBoundItem as HWCfgFileModel;
            this.HWFile = model;
            txtFileName.Text = model.HWName;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtFileName.Text.IsEmpty())
            {
                MessageHelper.Warn("请选择文件！");
                return;
            }
            //if (dgFiles.SelectedRows == null || dgFiles.SelectedRows.Count == 0)
            //    return;

            var model = dgFiles.SelectedRows[0].DataBoundItem as HWCfgFileModel;
            this.DialogResult = DialogResult.OK;
            new FrmMain(HWFile).ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
