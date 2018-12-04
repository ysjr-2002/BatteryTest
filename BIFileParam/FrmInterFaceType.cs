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
    /// <summary>
    /// 接口配置
    /// </summary>
    public partial class FrmInterFaceType : FixForm
    {
        private string parameter = "";
        public FrmInterFaceType(string parameter)
        {
            InitializeComponent();
            this.parameter = parameter;
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

            var array = parameter.Split(',');

            cmbPorts.SelectedIndex = (array[0].ToInt32() - 1);
            cmbBaud.Text = array[1];
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var tempate_str = "{0},{1},{2},{3},{4}";
            var str = "";

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
