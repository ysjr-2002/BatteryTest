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
        private TreeNode root = null;

        public FrmMain()
        {
            InitializeComponent();

            dgModel.AutoGenerateColumns = false;
            dgModule.AutoGenerateColumns = false;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            root = new TreeNode("目录树");
            root.ContextMenuStrip = rootContextMenuStrip;
            tvTree.Nodes.Add(root);
        }

        private async void tvTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var list = await AccessDBHelper.HWCfgModelList();
            dgModel.DataSource = list;
        }

        private void tsmiInstrument_Click(object sender, EventArgs e)
        {
            var window = new FrmInstrumentType();
            var dialog = window.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                var node = new TreeNode(window.InstrumentName);
                node.ContextMenuStrip = FZContextMenuStrip;
                root.Nodes.Add(node);
                if (root.IsExpanded == false)
                {
                    root.Expand();
                }
            }
        }

        private void tsmiAddFZ_Click(object sender, EventArgs e)
        {
            var window = new FrmFZType();
            var dialog = window.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                var node = new TreeNode(window.FZType);
                node.ContextMenuStrip = FZContextMenuStrip;

                var parent = tvTree.SelectedNode;
                parent.Nodes.Add(node);
                if (parent.IsExpanded == false)
                {
                    parent.Expand();
                }
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

        public static ContextMenuStrip getFJ(TreeNode node)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            node.Tag = node;
            return menu;
        }
    }
}
