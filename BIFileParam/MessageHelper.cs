using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIFileParam
{
    /// <summary>
    /// 消息框
    /// </summary>
    public static class MessageHelper
    {
        public static DialogResult Warn(string text)
        {
            return MessageBox.Show(text, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult Confirm(string text)
        {
            return MessageBox.Show(text, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
    }
}
