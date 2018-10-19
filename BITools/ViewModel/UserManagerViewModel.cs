using BIDataAccess.entities;
using BILogic;
using BIModel;
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
    public class UserManagerViewModel : BaseViewModel
    {
        private UserImpl userImpl;
        private UserInfo selectedUser = null;
        private const string PASSWORD_DEFAULT = "123456";

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

        public ICommand CreateCommand { get { return new DelegateCommand(CreateUser); } }
        public ICommand DeleteCommand { get { return new DelegateCommand(DeleteUser); } }
        public ICommand DetailCommand { get { return new DelegateCommand<UserInfo>(DetailUser); } }
        public ICommand PassWordResetCommand { get { return new DelegateCommand(PassWordReset); } }

        private void CreateUser()
        {
            if (Name.IsEmpty())
            {
                MsgBox.WarningShow("请输入登录用户名！");
                return;
            }

            UserInfo user = new UserInfo();
            user.UserName = Name;
            user.Password = PASSWORD_DEFAULT;
            user.CreateTime = DateTime.Now;
            user.Permission = (IsUser ? 1 : 0) | (IsConfig ? 2 : 0) | (IsDevice ? 4 : 0) | (IsHistory ? 8 : 0);

            bool flag = userImpl.CreateUser(user);
            Loaded();
        }

        private void DetailUser(UserInfo user)
        {
            selectedUser = user;
            Name = user.UserName;

            IsUser = ((((int)SystemModule.User) & user.Permission) == (int)SystemModule.User);
            IsConfig = ((((int)SystemModule.Config) & user.Permission) == (int)SystemModule.Config);
            IsDevice = ((((int)SystemModule.Device) & user.Permission) == (int)SystemModule.Device);
            IsHistory = ((((int)SystemModule.History) & user.Permission) == (int)SystemModule.History);
        }

        private void DeleteUser()
        {
            if (selectedUser == null)
                return;

            userImpl.DeleteUser(selectedUser);
            selectedUser = null;

            Loaded();
            ResetValue();
        }

        private void PassWordReset()
        {
            if (selectedUser == null)
                return;

            selectedUser.Password = PASSWORD_DEFAULT;
            userImpl.UpdateUser(selectedUser);
            ResetValue();
        }

        protected override void Loaded()
        {
            var list = userImpl.getUser();
            UserCollection = new ObservableCollection<UserInfo>(list);
        }

        private void ResetValue()
        {
            selectedUser = null;
            Name = string.Empty;
            IsUser = false;
            IsConfig = false;
            IsDevice = false;
            IsHistory = false;
        }
    }
}
