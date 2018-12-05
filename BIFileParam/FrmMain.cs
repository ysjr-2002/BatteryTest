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

        public FrmMain(string filename = "test")
        {
            InitializeComponent();

            this.filename = filename;
            dgModel.AutoGenerateColumns = false;
            dgModule.AutoGenerateColumns = false;
        }

        private async void FrmMain_Load(object sender, EventArgs e)
        {
            root = new TreeNode("仪器-(" + filename + ")");
            root.ImageIndex = 0;
            root.ContextMenuStrip = rootContextMenuStrip;
            tvTree.Nodes.Add(root);

            dgModel.TopLeftHeaderCell.Value = "Device No.";
            dgModule.TopLeftHeaderCell.Value = "Model No.";

            //加载仪器
            var subList = await AccessDBHelper.GetInstrumentModelList(filename);
            foreach (var item in subList)
            {
                var node = new TreeNode(item.InstrumentName);
                node.ContextMenuStrip = GetSecondContextMenuStrip(node);
                root.Nodes.Add(node);
            }
            root.ExpandAll();
        }

        private async void tvTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var name = e.Node.Text;
            if (name == "负载")
            {

            }
            else if (name == "水流控制卡")
            {

            }
            else if (name == "温度采集卡")
            {

            }
            else if (name == "温度控制卡")
            {

            }
            else if (name == "电气控制卡")
            {

            }
            else if (name == "电压电流采集卡")
            {

            }
            var list = await AccessDBHelper.HWCfgModelList();
            dgModel.DataSource = list;
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

        private void tsmiAddFZ_Click(object sender, EventArgs e)
        {
            var memuItem = sender as ToolStripItem;
            var node = memuItem.Tag as TreeNode;

            if (node.Text == "负载")
            {
                var window = new FrmFZType();
                var dialog = window.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    var subNode = new TreeNode(window.FZType);
                    subNode.ContextMenuStrip = GetDeleteContextMenuStrip(node);
                    node.Nodes.Add(subNode);
                    if (node.IsExpanded == false)
                    {
                        node.Expand();
                    }
                    var model = new HWCfgModel();
                    model.ModelName = subNode.Text;
                    model.HWName = filename;
                    model.InstrumentName = node.Text;
                    AccessDBHelper.Insert(model);
                }
            }
            else if (node.Text == "电气控制卡")
            {

            }
            else if (node.Text == "温度控制卡")
            {

            }
            else if (node.Text == "温度采集卡")
            {

            }
            else if (node.Text == "水流控制卡")
            {

            }
        }

        private void tsmiDelFZ_Click(object sender, EventArgs e)
        {
            var node = tvTree.SelectedNode;
            if (node != null)
            {
                root.Nodes.Remove(node);
            }
        }

        public ContextMenuStrip GetSecondContextMenuStrip(TreeNode node)
        {
            ToolStripItem add = new ToolStripMenuItem("添加" + node.Text);
            ToolStripItem del = new ToolStripMenuItem("删除" + node.Text);
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
            ToolStripItem del = new ToolStripMenuItem("删除" + node.Text);
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add(del);

            del.Click += Del_Click;
            del.Tag = node;
            return menu;
        }

        private void Del_Click(object sender, EventArgs e)
        {
            var memuItem = sender as ToolStripItem;
            var node = memuItem.Tag as TreeNode;
            var parent = node.Parent;
            if (parent != null)
            {
                parent.Nodes.Remove(node);
            }
        }

        private void dgModel_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    e.CellStyle.BackColor = ColorTranslator.FromHtml("#FFFFE1");
                }

                if (e.ColumnIndex == 0)
                {
                    dgModel.Rows[e.RowIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgModel.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                }
            }
        }

        private void dgModel_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var model = dgModel.Rows[e.RowIndex].DataBoundItem as HWCfgModel;
                var window = new FrmInterFaceType(model.InterfaceParameter);
                var dialog = window.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                }
            }
        }

        private void dgModule_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2)
                {
                    e.CellStyle.BackColor = ColorTranslator.FromHtml("#FFFFE1");
                }

                if (e.ColumnIndex == 0)
                {
                    dgModule.Rows[e.RowIndex].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgModule.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
                }
            }
        }
    }
}
