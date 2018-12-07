using BICommon;
using BIModel.Access;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
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
        private HWCfgModel model;
        private string parameter = "";

        public FrmInterFaceType(HWCfgModel model)
        {
            InitializeComponent();
            this.model = model;
            this.parameter = model.InterfaceParameter;
        }

        private void FrmInterFaceType_Load(object sender, EventArgs e)
        {
            foreach (var item in FunExt.GetSerialPorts())
            {
                cmbPorts.Items.Add(item);
            }

            foreach (var item in FunExt.Bauds())
            {
                cmbBaud.Items.Add(item);
            }

            cmbPorts.SelectedIndex = 0;
            cmbBaud.SelectedIndex = 0;
            cmbCheck.SelectedIndex = 0;
            cmbDataBit.SelectedIndex = 0;
            cmbStopBit.SelectedIndex = 0;

            var array = parameter.Split(new char[] { ',', ';' });
            cmbPorts.SelectedIndex = (array[0].ToInt32() - 1);
            cmbBaud.Text = array[1];
            cmbCheck.SelectedIndex = array[2].ToInt32();
            cmbDataBit.Text = array[3];
            cmbStopBit.SelectedIndex = array[4].ToInt32();

            if (array.Length >= 6)
            {
                nudAdd.Value = array[5].ToInt32();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var tempate_str = "{0};{1},{2},{3},{4};{5}";
            var str = string.Format(tempate_str,
                cmbPorts.Text.Substring(3),
                cmbBaud.Text,
                cmbCheck.SelectedIndex,
                cmbDataBit.Text,
                cmbStopBit.SelectedIndex,
                nudAdd.Value);

            AccessDBHelper.UpdateModel(model);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
