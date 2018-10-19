﻿using BILogic;
using BIModel;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BITools.ViewModel
{
    /// <summary>
    /// 修改密码ViewModel
    /// </summary>
    public class PassWordViewModel : BaseViewModel
    {
        private UserImpl userImpl;

        public PassWordViewModel()
        {
            userImpl = new UserImpl();
        }

        public string Name
        {
            get { return this.GetValue(c => c.Name); }
            set { this.SetValue(c => c.Name, value); }
        }

        public string OldPassWord
        {
            get { return this.GetValue(c => c.OldPassWord); }
            set { this.SetValue(c => c.OldPassWord, value); }
        }

        public string NewPassWord
        {
            get { return this.GetValue(c => c.NewPassWord); }
            set { this.SetValue(c => c.NewPassWord, value); }
        }

        public string NewPassWordConfirm
        {
            get { return this.GetValue(c => c.NewPassWordConfirm); }
            set { this.SetValue(c => c.NewPassWordConfirm, value); }
        }

        public ICommand SaveCommand { get { return new DelegateCommand(Save); } }

        private void Save()
        {
            if (OldPassWord.IsEmpty())
            {
                MsgBox.WarningShow("请输入旧密码！");
                return;
            }
            if (NewPassWord.IsEmpty())
            {
                MsgBox.WarningShow("请输入新密码！");
                return;
            }

            if (NewPassWord != NewPassWordConfirm)
            {
                MsgBox.WarningShow("新密码和确认密码不同！");
                return;
            }

            var flag = userImpl.UpdatePassWord(AppContext.UserId, NewPassWord);
            if (flag)
            {
                MsgBox.SuccessShow("密码修改成功！");
            }
        }

        protected override void Loaded()
        {
            this.Name = AppContext.UserName = "test";
        }
    }
}
