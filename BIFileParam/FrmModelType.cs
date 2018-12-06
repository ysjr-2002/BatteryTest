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
    /// 
    /// </summary>
    public partial class FrmModelType : Form
    {
        private string instrumentname = "";
        public FrmModelType(string nodetag)
        {
            InitializeComponent();
            this.instrumentname = nodetag.Split('_')[1];
        }

        public ModelList ModelList { get; set; }

        private async void FrmFZType_Load(object sender, EventArgs e)
        {
            var list = await AccessDBHelper.GetModelList();
            var modelList = list.Where(s => s.InstrumentName == instrumentname).ToList();
            foreach (var model in modelList)
            {
                lbModel.Items.Add(model);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lbModel.SelectedItem != null)
            {
                ModelList = lbModel.SelectedItem as ModelList;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
