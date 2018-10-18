using BIDataAccess.entities;
using BILogic;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BITools.ViewModel
{
    public class UserManagerViewModel : PropertyNotifyObject
    {
        private UserImpl userImpl;

        public UserManagerViewModel()
        {
            userImpl = new UserImpl();
        }

        public ObservableCollection<UserInfo> UserCollection
        {
            get { return this.GetValue(c => c.UserCollection); }
            set { this.SetValue(c => c.UserCollection, value); }
        }

        public string Name
        {
            get { return this.GetValue(c => c.Name); }
            set { this.SetValue(c => c.Name, value); }
        }

        public bool IsUser
        {
            get { return this.GetValue(c => c.IsUser); }
            set { this.SetValue(c => c.IsUser, value); }
        }

        public bool IsConfig
        {
            get { return this.GetValue(c => c.IsConfig); }
            set { this.SetValue(c => c.IsConfig, value); }
        }

        public bool IsDevice
        {
            get { return this.GetValue(c => c.IsDevice); }
            set { this.SetValue(c => c.IsDevice, value); }
        }

        public bool IsHistory
        {
            get { return this.GetValue(c => c.IsHistory); }
            set { this.SetValue(c => c.IsHistory, value); }
        }

        public ICommand LoadedCommand { get { return new DelegateCommand(QueryUser); } }
        public ICommand CreateCommand { get { return new DelegateCommand(CreateUser); } }
        public ICommand DeleteCommand { get { return new DelegateCommand(DeleteUser); } }
        public ICommand DetailCommand { get { return new DelegateCommand<UserInfo>(DetailUser); } }

        public ICommand PasswordResetCommand { get { return new DelegateCommand(PasswordReset); } }

        private void CreateUser()
        {
            if (Name.IsEmpty())
            {
                MsgBox.WarningShow("请输入用户名称！");
                return;
            }

            UserInfo user = new UserInfo();
            user.UserName = Name;
            user.Password = "123456";
            user.CreateTime = DateTime.Now;
            user.Permission = (IsUser ? 1 : 0) | (IsConfig ? 2 : 0) | (IsDevice ? 4 : 0) | (IsHistory ? 8 : 0);

            bool flag = userImpl.CreateUser(user);
            QueryUser();
        }

        private void DetailUser(UserInfo user)
        {

        }

        private void DeleteUser()
        {

        }

        private void PasswordReset()
        {

        }

        private void QueryUser()
        {
            var list = userImpl.getUser();
            UserCollection = new ObservableCollection<UserInfo>(list);
        }
    }
}
