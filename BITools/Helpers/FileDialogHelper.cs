using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.Helpers
{
    /// <summary>
    /// 对话框选择
    /// </summary>
    static class FileDialogHelper
    {
        /// <summary>
        /// 设备参数对话选择框
        /// </summary>
        /// <returns></returns>
        public static string OpenFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择参数";
            ofd.Filter = "测试参数(*.yhk)|*.yhk";
            ofd.Multiselect = false;
            var dialog = ofd.ShowDialog().GetValueOrDefault();
            if (dialog)
            {
                return ofd.FileName;
            }
            return string.Empty;
        }
    }
}
