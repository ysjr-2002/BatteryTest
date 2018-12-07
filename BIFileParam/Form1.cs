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
    public partial class Form1 : Form
    {
        static Pen gridLinePen = new Pen(Color.Red);

        public Form1()
        {
            InitializeComponent();
        }

        List<Temp> list = new List<Temp>();
        private void Form1_Load(object sender, EventArgs e)
        {
            list.Add(new Temp { id = "1", name = "ysj", gradle = "语文" });
            list.Add(new Temp { id = "1", name = "ysj", gradle = "数学" });
            list.Add(new Temp { id = "1", name = "ysj", gradle = "英语" });

            list.Add(new Temp { id = "2", name = "dgl", gradle = "语文" });
            list.Add(new Temp { id = "2", name = "dgl", gradle = "数学" });
            list.Add(new Temp { id = "3", name = "dgl", gradle = "英语" });

            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = true;
            dgv.DataSource = list;
        }

        static string[] colsHeaderText_V = new string[] { "编号", "名称" };
        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 对第1列相同单元格进行合并
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                using
                    (
                    Brush gridBrush = new SolidBrush(this.dgv.GridColor),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor)
                    )
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        // 清除单元格
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        e.CellStyle.SelectionBackColor = Color.Blue;

                        // 画 Grid 边线（仅画单元格的底边线和右边线）
                        //   如果下一行和当前行的数据不同，则在当前的单元格画一条底边线
                        if (e.RowIndex < dgv.Rows.Count - 1 &&
                        dgv.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                        {
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                            e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom - 1);
                        }
                        // 画右边线
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                            e.CellBounds.Top, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom);

                        //// 画（填写）单元格内容，相同的内容的单元格只填写第一个
                        //if (e.Value != null)
                        //{
                        //    if (e.RowIndex > 0 && dgv.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString())
                        //    { }
                        //    else
                        //    {
                        //        e.Graphics.DrawString((String)e.Value, e.CellStyle.Font,
                        //            Brushes.Black, e.CellBounds.X + 15,
                        //            e.CellBounds.Y + 10, StringFormat.GenericDefault);
                        //    }
                        //}

                        if (dgv.Rows[e.RowIndex].Selected)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(e.CellStyle.SelectionBackColor), e.CellBounds);
                        }

                        e.Handled = true;
                    }
                }
            }
        }
    }

    class Temp
    {
        public string id { get; set; }

        public string name { get; set; }

        public string gradle { get; set; }
    }
}
