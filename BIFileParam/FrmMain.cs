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
        TreeNode root = null;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            root = new TreeNode("目录树");
            root.ContextMenuStrip = rootContextMenuStrip;
            tvTree.Nodes.Add(root);
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
            }
        }

        private void tsmiAddFZ_Click(object sender, EventArgs e)
        {
           
        }

        private void tsmiDelFZ_Click(object sender, EventArgs e)
        {
            TreeNode node = sender as TreeNode;
            root.Nodes.Remove(node);
        }
    }
}
