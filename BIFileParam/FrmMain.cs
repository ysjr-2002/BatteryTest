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
    public partial class FrmMain : Form
    {
        private string filename = "";
        private TreeNode root = null;
        private static readonly char splitchar = '_';
        private static readonly string fix_add = "添加 ";
        private static readonly string fix_del = "删除 ";
        private static readonly string model_del_template = "确认删除 '{0}' 和它所有子类型?";
        private static readonly string model_del_all_template = "确认删除所有模型 '{0}'?";

        private static readonly Color fixColor = ColorTranslator.FromHtml("#FFFFE1");

        public FrmMain(string filename = "test")
        {
            InitializeComponent();
            this.filename = filename;
            dgModel.AutoGenerateColumns = false;
            dgModule.AutoGenerateColumns = false;
            BuildActiveCombobox();
        }

        private void BuildActiveCombobox()
        {
            cmbActive.Items.Add("True");
            cmbActive.Items.Add("False");
            cmbActive.Visible = false;
            cmbActive.DrawMode = DrawMode.OwnerDrawVariable;
            cmbActive.DrawItem += new DrawItemEventHandler(this.cboBoxPostID_DrawItem);
            cmbActive.SelectedIndexChanged += cmbActive_SelectedIndexChanged;
            cmbActive.LostFocus += cmbActive_LostFocus;
        }

        private void cmbActive_LostFocus(object sender, EventArgs e)
        {
            cmbActive.Visible = false;
        }

        private void cmbActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgModel.CurrentCell.Value = Util.GetActiveBoolToValDisplay(cmbActive.Text);
        }

        private void cboBoxPostID_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(cmbActive.GetItemText(cmbActive.Items[e.Index]).ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 3);
        }

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            root = new TreeNode("仪器-(" + filename + ")");
            root.Tag = filename;
            root.ImageIndex = 0;
            root.ContextMenuStrip = rootContextMenuStrip;
            tvTree.Nodes.Add(root);

            dgModel.TopLeftHeaderCell.Value = "Device No.";
            dgModule.TopLeftHeaderCell.Value = "Model No.";

            //加载仪器
            var instrumentList = await AccessDBHelper.GetInstrumentModelList(filename);
            foreach (var item in instrumentList)
            {
                var node = new TreeNode(item.InstrumentName);
                node.Tag = filename + splitchar + item.InstrumentName;
                node.ContextMenuStrip = GetSecondContextMenuStrip(node);
                root.Nodes.Add(node);

                var deviceList = await AccessDBHelper.HWCfgModelListByInstrumentName(filename, item.InstrumentName);
                foreach (var device in deviceList)
                {
                    var subnode = new TreeNode(device.ModelName);
                    subnode.Tag = device;
                    subnode.ContextMenuStrip = GetDeleteContextMenuStrip(subnode);
                    node.Nodes.Add(subnode);
                }
            }
            root.Expand();
        }

        private async void tvTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = e.Node;
            if (node.Tag.GetType() == typeof(string))
            {
                var tag = node.Tag.ToString();
                var array = tag.Split(splitchar);
                if (array.Length == 1)
                {
                    var deviceList = await AccessDBHelper.HWCfgModelListByHWName(array[0]);
                    dgModel.DataSource = deviceList;
                }
                else
                {
                    var deviceList = await AccessDBHelper.HWCfgModelListByInstrumentName(array[0], array[1]);
                    dgModel.DataSource = deviceList;
                    dgModule.DataSource = await AccessDBHelper.HWCfgModuleListByInstrumentName(array[0], array[1]);
                }
            }
            else
            {
                var model = (HWCfgModel)node.Tag;
                dgModel.DataSource = new HWCfgModel[] { model };
                dgModule.DataSource = await AccessDBHelper.HWCfgModuleListByInstrumentName(model.HWName, model.InstrumentName, model.ModelName);
            }
        }

        /// <summary>
        /// 添加仪器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiInstrumentAdd_Click(object sender, EventArgs e)
        {
            var window = new FrmInstrumentType();
            var dialog = window.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                var node = new TreeNode(window.InstrumentName);
                node.ContextMenuStrip = GetSecondContextMenuStrip(node);
                root.Nodes.Add(node);
                if (root.IsExpanded == false)
                {
                    root.Expand();
                }

                var model = new HWCfgInstrumentModel
                {
                    InstrumentName = window.InstrumentName,
                    HWName = filename,
                };
                node.Tag = filename + splitchar + node.Text;
                AccessDBHelper.Insert(model);
            }
        }

        private void tsmiInstrumentDelAll_Click(object sender, EventArgs e)
        {
            if (MessageHelper.Confirm("确认删除所有仪器吗？") == DialogResult.Yes)
            {
                AccessDBHelper.DeleteInstrumentByHWName(filename);
                AccessDBHelper.DeleteModelByHWName(filename);
                AccessDBHelper.DeleteModuleByHWName(filename);
                root.Nodes.Clear();
            }
        }

        private async void tsmiAddFZ_Click(object sender, EventArgs e)
        {
            var memuItem = sender as ToolStripItem;
            var node = memuItem.Tag as TreeNode;

            var window = new FrmModelType(node.Tag.ToString());
            var dialog = window.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                var modelList = window.ModelList;
                var subNode = new TreeNode(modelList.ModelName);

                var array = node.Tag.ToString().Split(splitchar);
                var model = new HWCfgModel();
                model.ModelName = modelList.ModelName;
                model.HWName = array[0];
                model.InstrumentName = array[1];
                model.Interface = modelList.InterfaceAvailable;
                model.InterfaceParameter = modelList.DefaultParameter;
                model.ModelIndex = node.Nodes.Count + 1;
                AccessDBHelper.Insert(model);

                subNode.Tag = model;
                subNode.ContextMenuStrip = GetDeleteContextMenuStrip(node);
                node.Nodes.Add(subNode);
                if (node.IsExpanded == false)
                {
                    node.Expand();
                }

                var deviceList = await AccessDBHelper.HWCfgModelListByInstrumentName(array[0], array[1]);
                dgModel.DataSource = deviceList;
            }
        }

        private void tsmiDelFZ_Click(object sender, EventArgs e)
        {
            var memuItem = sender as ToolStripItem;
            var node = memuItem.Tag as TreeNode;

            var array = node.Tag.ToString().Split(splitchar);
            var question = string.Format(model_del_all_template, array[1]);
            var dialog = MessageHelper.Confirm(question);
            if (dialog == DialogResult.Yes)
            {
                node.Nodes.Clear();
                AccessDBHelper.DeleteModel(array[0], array[1]);
                AccessDBHelper.DeleteModule(array[0], array[1]);
                dgModel.DataSource = null;
                dgModule.DataSource = null;
            }
        }

        public ContextMenuStrip GetSecondContextMenuStrip(TreeNode node)
        {
            ToolStripItem add = new ToolStripMenuItem(fix_add + node.Text);
            ToolStripItem del = new ToolStripMenuItem(fix_del + node.Text);
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add(add);
            menu.Items.Add(del);

            add.Click += tsmiAddFZ_Click;
            del.Click += tsmiDelFZ_Click;

            add.Tag = node;
            del.Tag = node;
            return menu;
        }

        public ContextMenuStrip GetDeleteContextMenuStrip(TreeNode node)
        {
            ToolStripItem tsmiDel = new ToolStripMenuItem(fix_del + node.Text);
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add(tsmiDel);

            tsmiDel.Click += tsmiDel_Click;
            tsmiDel.Tag = node;
            return menu;
        }

        private async void tsmiDel_Click(object sender, EventArgs e)
        {
            var memuItem = sender as ToolStripItem;
            var node = memuItem.Tag as TreeNode;
            var dialog = MessageHelper.Confirm(string.Format(model_del_template, node.Text));
            if (dialog == DialogResult.Yes)
            {
                var parent = node.Parent;
                if (parent != null)
                {
                    parent.Nodes.Remove(node);
                }
                var model = node.Tag as HWCfgModel;
                AccessDBHelper.DeleteModel(model);

                var array = parent.Tag.ToString().Split(splitchar);
                var deviceList = await AccessDBHelper.HWCfgModelListByInstrumentName(array[0], array[1]);
                dgModel.DataSource = deviceList;
                dgModule.DataSource = await AccessDBHelper.HWCfgModuleListByInstrumentName(array[0], array[1]);
            }
        }

        private void dgModel_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    e.CellStyle.BackColor = fixColor;
                }

                if (e.ColumnIndex == 0)
                {
                    dgModel.Rows[e.RowIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgModel.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                }

                if (dgModel.Columns[e.ColumnIndex].DataPropertyName == "Active")
                {
                    e.Value = Util.GetActiveValToBoolDisplay(e.Value.ToString());
                }
            }
        }

        private async void dgModel_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0)
                return;

            if (e.ColumnIndex <= 2)
            {
                var source = dgModel.Rows[e.RowIndex].DataBoundItem as HWCfgModel;
                var modulelist = await AccessDBHelper.HWCfgModuleListByInstrumentName(source.HWName, source.InstrumentName, source.ModelName);
                dgModule.DataSource = modulelist;
            }

            if (dgModel.Columns[e.ColumnIndex].DataPropertyName == "Active")
            {
                var rect = dgModel.GetCellDisplayRectangle(dgModel.CurrentCell.ColumnIndex, dgModel.CurrentCell.RowIndex, false);
                cmbActive.Left = rect.Left;
                cmbActive.Top = rect.Top;
                cmbActive.Width = rect.Width;
                cmbActive.Height = rect.Height;
                cmbActive.Visible = true;
                cmbActive.Focus();
                dgModel.CurrentCell.Style.SelectionBackColor = Color.White;
                var sexValue = dgModel.CurrentCell.Value.ToString();
                cmbActive.Text = Util.GetActiveValToBoolDisplay(sexValue);
            }
        }

        private void dgModel_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgModel.Columns[e.ColumnIndex].DataPropertyName == "InterfaceParameter")
                {
                    var model = dgModel.Rows[e.RowIndex].DataBoundItem as HWCfgModel;
                    var window = new FrmInterFaceType(model);
                    var dialog = window.ShowDialog();
                    if (dialog == DialogResult.OK)
                    {
                    }
                }
            }
        }

        private void dgModule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    e.CellStyle.BackColor = fixColor;
                }

                if (e.ColumnIndex == 0)
                {
                    dgModule.Rows[e.RowIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgModule.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                }

                if (dgModule.Columns[e.ColumnIndex].DataPropertyName == "Active")
                {
                    e.Value = Util.GetActiveValToBoolDisplay(e.Value.ToString());
                }
            }
        }

        private void dgModel_Scroll(object sender, ScrollEventArgs e)
        {
            cmbActive.Visible = false;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            var dialog = MessageHelper.ConfirmEx("确认退出?");
            if (dialog == DialogResult.OK)
            {
                this.Close();
            }
        }
    }
}
