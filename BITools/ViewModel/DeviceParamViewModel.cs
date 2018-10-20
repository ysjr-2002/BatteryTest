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
    public class DeviceParamViewModel : BaseViewModel
    {
        public DeviceParamViewModel()
        {
            ChannelCollection = new ObservableCollection<ChannelInfo>();
        }

        protected override void Loaded()
        {
            base.Loaded();

            var dictImpl = new DictonaryImpl();

            var list = dictImpl.QueryDictionary("TDBL");
            TDBLCollection = new ObservableCollection<Dictonary>(list);

            list = dictImpl.QueryDictionary("SRDY");
            SRDYCollection = new ObservableCollection<Dictonary>(list);
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
    }
}
