using BIDataAccess.entities;
using BILogic;
using BIModel;
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
    public class DeviceParamViewModel : BaseViewModel
    {
        public DeviceParamViewModel()
        {
            ChannelCollection = new ObservableCollection<ChannelInfo>();
            JiaRe = "10";
            ShaoJi = "20";
            PaiFeng = "30";
            BaoJing = "40";
        }

        protected override void Loaded()
        {
            base.Loaded();

            var dictImpl = new DictonaryImpl();

            var list = dictImpl.QueryDictionary("TDBL");
            TDBLCollection = new ObservableCollection<Dictonary>(list);

            list = dictImpl.QueryDictionary("SRDY");
            SRDYCollection = new ObservableCollection<Dictonary>(list);

            list = dictImpl.QueryDictionary("FZCS");
            FZCSCollection = new ObservableCollection<Dictonary>(list);
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

        public ObservableCollection<Dictonary> TDBLCollection
        {
            get { return this.GetValue(c => c.TDBLCollection); }
            set { this.SetValue(c => c.TDBLCollection, value); }
        }

        public ObservableCollection<ChannelInfo> ChannelCollection
        {
            get { return this.GetValue(c => c.ChannelCollection); }
            set { this.SetValue(c => c.ChannelCollection, value); }
        }

        public ObservableCollection<Dictonary> SRDYCollection
        {
            get { return this.GetValue(c => c.SRDYCollection); }
            set { this.SetValue(c => c.SRDYCollection, value); }
        }

        public ObservableCollection<Dictonary> FZCSCollection
        {
            get { return this.GetValue(c => c.FZCSCollection); }
            set { this.SetValue(c => c.FZCSCollection, value); }
        }

        public ObservableCollection<Dictonary> PDFWCollection
        {
            get { return this.GetValue(c => c.PDFWCollection); }
            set { this.SetValue(c => c.PDFWCollection, value); }
        }

        public ICommand AddChannelCommand { get { return new DelegateCommand(AddChannel); } }
        public ICommand RemoveAllChannelCommand { get { return new DelegateCommand(RemoveAllChannel); } }

        public ICommand AddLHSXCommand { get { return new DelegateCommand(AddLHSX); } }
        public ICommand ModifyLHSXCommand { get { return new DelegateCommand(ModifyLHSX); } }
        public ICommand DeleteLHSXCommand { get { return new DelegateCommand(DeleteLHSX); } }
        public ICommand ResetLHSXCommand { get { return new DelegateCommand(ResetLHSX); } }


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
            ChannelCollection.Add(channel);
        }

        private void AddLHSX()
        {

        }

        private void ModifyLHSX()
        {

        }

        private void DeleteLHSX()
        {

        }

        private void ResetLHSX()
        {

        }

        string paramFilePath = "";
        private void LoadParam()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择参数";
            ofd.Filter = "测试参数(*.yhk)|*.yhk";
            ofd.Multiselect = false;
            var dialog = ofd.ShowDialog().GetValueOrDefault();
            if (dialog)
            {
                paramFilePath = ofd.FileName;
            }
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
