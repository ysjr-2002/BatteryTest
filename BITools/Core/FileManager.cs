using Common;
using LL.SenicSpot.Gate.Kernal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITools.Core
{
    public static class FileManager
    {
        private static string root = "photo";

        static FileManager()
        {
            root = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, root);
            if (Directory.Exists(root) == false)
                Directory.CreateDirectory(root);
        }

        public static string ReadFile(string filepath)
        {
            if (File.Exists(filepath))
                return File.ReadAllText(filepath);
            else
                return string.Empty;
        }

        static string GetFolder()
        {
            var day = DateTime.Now.ToString("yyyyMMdd");
            var folder = Path.Combine(root, day);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return folder;
        }

        public static string OpenParamFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "打开参数";
            ofd.Filter = "*.tk(*.tk)|*.tk";
            var dialog = ofd.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                return ofd.FileNames.First();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string SaveParamFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "保存参数";
            sfd.Filter = "*.tk(*.tk)|*.tk";
            var dialog = sfd.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                return sfd.FileNames.First();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}