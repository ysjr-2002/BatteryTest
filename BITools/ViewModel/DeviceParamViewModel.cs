using BIDataAccess.entities;
using BILogic;
using BIModel;
using BITools.Core;
using BITools.Helpers;
using BITools.Model;
using BITools.ViewModel.LHSX;
using Common.NotifyBase;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BITools.ViewModel
{
    /// <summary>
    /// 设备参数ViewModel
    /// </summary>
    public class DeviceParamViewModel : BaseViewModel
    {
        public DeviceParamViewModel()
        {
            JiaRe = "10";
            ShaoJi = "20";
            PaiFeng = "30";
            BaoJing = "40";
            LHSXViewModel = new LHSXViewModel();
        }

        public override void Loaded()
        {
            base.Loaded();

            LHSXViewModel.Loaded();

            var dictImpl = new DictonaryImpl();

            var list = dictImpl.QueryDictionary("TDBL");
            TDBLCollection = new ObservableCollection<Dictonary>(list);
            list = dictImpl.QueryDictionary("CSMS");
            CSMSCollection = new ObservableCollection<Dictonary>(list);
            ChannelCollection = new ObservableCollection<ChannelInfo>();
        }

        public string JiaRe
        {
            get { return this.GetValue(c => c.JiaRe); }
            set { this.SetValue(c => c.JiaRe, value); }
        }

        public string ShaoJi
        {
            get { return this.GetValue(c => c.ShaoJi); }
            set { this.SetValue(c => c.ShaoJi, value); }
        }

        public string PaiFeng
        {
            get { return this.GetValue(c => c.PaiFeng); }
            set { this.SetValue(c => c.PaiFeng, value); }
        }

        public string BaoJing
        {
            get { return this.GetValue(c => c.BaoJing); }
            set { this.SetValue(c => c.BaoJing, value); }
        }

        /// <summary>
        /// 通道并联
        /// </summary>
        public ObservableCollection<Dictonary> TDBLCollection
        {
            get { return this.GetValue(c => c.TDBLCollection); }
            set { this.SetValue(c => c.TDBLCollection, value); }
        }

        /// <summary>
        /// 测试模式
        /// </summary>
        public ObservableCollection<Dictonary> CSMSCollection
        {
            get { return this.GetValue(c => c.CSMSCollection); }
            set { this.SetValue(c => c.CSMSCollection, value); }
        }

        //public Dictonary CSMSSelectedItem
        //{
        //    get { return this.GetValue(c => c.CSMSSelectedItem); }
        //    set { this.SetValue(c => c.CSMSSelectedItem, value); }
        //}

        /// <summary>
        /// 通道集合
        /// </summary>
        public ObservableCollection<ChannelInfo> ChannelCollection
        {
            get { return this.GetValue(c => c.ChannelCollection); }
            set { this.SetValue(c => c.ChannelCollection, value); }
        }

        public LHSXViewModel LHSXViewModel
        {
            get { return this.GetValue(c => c.LHSXViewModel); }
            set { this.SetValue(c => c.LHSXViewModel, value); }
        }

        public ICommand AddChannelCommand { get { return new DelegateCommand(AddChannel); } }
        public ICommand RemoveAllChannelCommand { get { return new DelegateCommand(RemoveAllChannel); } }


        public ICommand LoadParamCommand { get { return new DelegateCommand(LoadParam); } }
        public ICommand SaveParamCommand { get { return new DelegateCommand(SaveParam); } }
        public ICommand SaveAsParamCommand { get { return new DelegateCommand(SaveAsParam); } }
        public ICommand UseParamCommand { get { return new DelegateCommand(UseParam); } }

        private void RemoveAllChannel()
        {
            if (MsgBox.QuestionShow("确认清空所有通道吗?") == MsgBoxResult.OK)
            {
                ChannelCollection?.Clear();
            }
        }

        private void AddChannel()
        {
            var channel = new ChannelInfo();
            channel.CSMSSelectedItem = CSMSCollection.First();
            ChannelCollection.Add(channel);
        }


        string paramFilePath = "";
        private void LoadParam()
        {
            paramFilePath = FileDialogHelper.OpenFileDialog();
        }

        private void SaveParam()
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Title = "保存参数";
            ofd.Filter = "测试参数(*.yhk)|*.yhk";
            var dialog = ofd.ShowDialog().GetValueOrDefault();
            if (dialog)
            {
                paramFilePath = ofd.FileName;
            }
        }

        private void SaveAsParam()
        {
            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Title = "另存参数";
            ofd.Filter = "测试参数(*.yhk)|*.yhk";
            var dialog = ofd.ShowDialog().GetValueOrDefault();
            if (dialog)
            {
                paramFilePath = ofd.FileName;
            }
        }

        private void UseParam()
        {
            var dialog = MsgBox.QuestionShow("确认应用参数到台车设备吗？");
            if (dialog != MsgBoxResult.OK)
                return;
        }
    }
}
