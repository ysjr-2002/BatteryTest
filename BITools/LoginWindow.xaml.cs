using BICommon;
using BIDataAccess.entities;
using BILogic;
using BIModel;
using BITools.DataManager;
using BITools.SystemManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BITools
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private const string temp_file = "battery.txt";

        private UserService userImpl;

        public LoginWindow()
        {
            InitializeComponent();
            userImpl = new UserService();
            this.Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string name = "";
            string pwd = "";
            bool remember = false;
            readFile(out name, out pwd, out remember);

            txtName.Text = name;
            txtPsw.Password = pwd;
            ckbRemember.IsChecked = remember;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var name = txtName.Text.Trim();
            var password = txtPsw.Password.Trim();
            if (name.IsEmpty())
            {
                MsgBox.WarningShow("请输入用户名！");
                return;
            }
            if (password.IsEmpty())
            {
                MsgBox.WarningShow("请输入密码！");
                return;
            }

            imgWait.Visibility = Visibility.Visible;
            btnLogin.IsEnabled = false;
            var user = await userImpl.Login(name, password);
            imgWait.Visibility = Visibility.Collapsed;
            btnLogin.IsEnabled = true;
            if (user != null)
            {
                AppContext.UserId = (int)user.UserID;
                AppContext.UserName = user.UserName;
                AppContext.PassWord = user.Password;
                saveFile(name, password, ckbRemember.IsChecked.GetValueOrDefault());
                this.Hide();
                var window = new MainWindow();
                window.ShowDialog();
            }
            else
            {
                MsgBox.WarningShow("请用户名或密码错误，请重新输入！");
            }
        }

        private static string getTempFileFullPath()
        {
            var root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var path = System.IO.Path.Combine(root, temp_file);
            return path;
        }

        private void saveFile(string name, string pwd, bool remember)
        {
            var path = getTempFileFullPath();
            if (File.Exists(path))
                File.Delete(path);
            if (remember)
            {
                string content = string.Format("{0}\n{1}\n{2}\n", name, pwd, "1");
                File.WriteAllText(path, content);
            }
        }

        private void readFile(out string name, out string pwd, out bool remember)
        {
            name = "";
            pwd = "";
            remember = false;
            var path = getTempFileFullPath();
            if (System.IO.File.Exists(path))
            {
                var lines = File.ReadLines(path);
                if (lines == null)
                    return;

                if (lines.Count() >= 1)
                    name = lines.First();

                if (lines.Count() >= 2)
                    pwd = lines.ElementAt(1);

                if (lines.Count() >= 3)
                    remember = (lines.ElementAt(2) == "1");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
