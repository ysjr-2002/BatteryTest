using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIFileParam
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmCfgList());
            Application.Run(new FrmMain(new BIModel.Access.HWCfgFileModel { HWName = "HangJia", HWTime = DateTime.Now, Author = "ysj", SystemDefault = 0 }));
        }
    }
}
