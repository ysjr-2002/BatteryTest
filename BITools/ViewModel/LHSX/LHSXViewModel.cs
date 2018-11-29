using BITools.Model;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.NotifyBase;
using BIDataAccess.entities;
using BILogic;
using BICommon.Enums;

namespace BITools.ViewModel.LHSX
{
    /// <summary>
    /// 老化时序ViewModel
    /// </summary>
    public class LHSXViewModel : BaseViewModel
    {
        public LHSXViewModel()
        {
            XHCS = "2";
            LHSXCollection = new ObservableCollection<LHSXInfo>();
        }

        /// <summary>
        /// 输入电压
        /// </summary>
        public ObservableCollection<Dictonary> SRDYCollection
        {
            get { return this.GetValue(c => c.SRDYCollection); }
            set { this.SetValue(c => c.SRDYCollection, value); }
        }

        public Dictonary SRDYSelectedItem
        {
            get { return this.GetValue(c => c.SRDYSelectedItem); }
            set { this.SetValue(c => c.SRDYSelectedItem, value); }
        }

        /// <summary>
        /// 负载参数
        /// </summary>
        public ObservableCollection<Dictonary> FZCSCollection
        {
            get { return this.GetValue(c => c.FZCSCollection); }
            set { this.SetValue(c => c.FZCSCollection, value); }
        }

        /// <summary>
        /// 负载参数
        /// </summary>
        public Dictonary FZCSSelectedItem
        {
            get { return this.GetValue(c => c.FZCSSelectedItem); }
            set { this.SetValue(c => c.FZCSSelectedItem, value); }
        }

        /// <summary>
        /// 判断范围
        /// </summary>
        //public ObservableCollection<Dictonary> PDFWCollection
        //{
        //    get { return this.GetValue(c => c.PDFWCollection); }
        //    set { this.SetValue(c => c.PDFWCollection, value); }
        //}

        public int PDFWSelectedIndex
        {
            get { return this.GetValue(c => c.PDFWSelectedIndex); }
            set { this.SetValue(c => c.PDFWSelectedIndex, value); }
        }

        /// <summary>
        /// 循环次数
        /// </summary>
        public string XHCS
        {
            get { return this.GetValue(c => c.XHCS); }
            set { this.SetValue(c => c.XHCS, value); }
        }

        public bool Iscj
        {
            get { return this.GetValue(c => c.Iscj); }
            set { this.SetValue(c => c.Iscj, value); }
        }

        public string DZZX
        {
            get { return this.GetValue(c => c.DZZX); }
            set { this.SetValue(c => c.DZZX, value); }
        }

        public int DZZXUnitSelectedIndex
        {
            get { return this.GetValue(c => c.DZZXUnitSelectedIndex); }
            set { this.SetValue(c => c.DZZXUnitSelectedIndex, value); }
        }

        public string GSC
        {
            get { return this.GetValue(c => c.GSC); }
            set { this.SetValue(c => c.GSC, value); }
        }

        public int GSCUnitSelectedIndex
        {
            get { return this.GetValue(c => c.GSCUnitSelectedIndex); }
            set { this.SetValue(c => c.GSCUnitSelectedIndex, value); }
        }

        public string KSC
        {
            get { return this.GetValue(c => c.KSC); }
            set { this.SetValue(c => c.KSC, value); }
        }

        public int KSCUnitSelectedIndex
        {
            get { return this.GetValue(c => c.KSCUnitSelectedIndex); }
            set { this.SetValue(c => c.KSCUnitSelectedIndex, value); }
        }

        /// <summary>
        /// 老化时序
        /// </summary>
        public ICommand AddLHSXCommand { get { return new DelegateCommand(AddLHSX); } }
        public ICommand ModifyLHSXCommand { get { return new DelegateCommand(ModifyLHSX); } }
        public ICommand DeleteLHSXCommand { get { return new DelegateCommand(DeleteLHSX); } }
        public ICommand ResetLHSXCommand { get { return new DelegateCommand(ResetLHSX); } }
        public ICommand EditLHSXCommand { get { return new DelegateCommand(EditLHSX); } }

        /// <summary>
        /// 老化时序集合
        /// </summary>
        public ObservableCollection<LHSXInfo> LHSXCollection
        {
            get { return this.GetValue(c => c.LHSXCollection); }
            set { this.SetValue(c => c.LHSXCollection, value); }
        }

        public LHSXInfo LHSXSelectedItem
        {
            get { return this.GetValue(c => c.LHSXSelectedItem); }
            set { this.SetValue(c => c.LHSXSelectedItem, value); }
        }

        public override void Loaded()
        {
            var dictImpl = new DictonaryService();

            var list = dictImpl.QueryDictionary("SRDY");
            SRDYCollection = new ObservableCollection<Dictonary>(list);
            SRDYSelectedItem = SRDYCollection.FirstOrDefault();

            list = dictImpl.QueryDictionary("FZCS");
            FZCSCollection = new ObservableCollection<Dictonary>(list);
            FZCSSelectedItem = FZCSCollection.FirstOrDefault();
        }

        private void AddLHSX()
        {
            LHSXInfo item = new LHSXInfo();
            item.srdy = SRDYSelectedItem.Value;
            item.fzcy = FZCSSelectedItem.Name;
            item.pdfw = getCSSXXName();
            item.cjkt = Iscj;
            item.dzzzTimeUnit = (RepeatUnitEnum)DZZXUnitSelectedIndex;
            item.dzzx = DZZX + "-" + FunExt.GetRepeatUnitName(DZZXUnitSelectedIndex);
            item.gscTimeUnit = (TimeUnitEnum)GSCUnitSelectedIndex;
            item.gsc = GSC + "-" + FunExt.GetTimeUnitName(GSCUnitSelectedIndex);
            item.kscTimeUnit = (TimeUnitEnum)KSCUnitSelectedIndex;
            item.ksc = KSC + "-" + FunExt.GetTimeUnitName(KSCUnitSelectedIndex);
            LHSXCollection.Add(item);
        }

        private void EditLHSX()
        {
            if (LHSXSelectedItem == null)
            {
                return;
            }

            SelectedRFDY();
            SelectedFZCS();
            SelectedCSSXX();
            Iscj = LHSXSelectedItem.cjkt;
            DZZX = LHSXSelectedItem.dzzx.Split('-')[0];
            DZZXUnitSelectedIndex = (int)LHSXSelectedItem.dzzzTimeUnit;

            GSC = LHSXSelectedItem.gsc.Split('-')[0];
            GSCUnitSelectedIndex = (int)LHSXSelectedItem.gscTimeUnit;

            KSC = LHSXSelectedItem.ksc.Split('-')[0];
            KSCUnitSelectedIndex = (int)LHSXSelectedItem.kscTimeUnit;
        }

        private void ModifyLHSX()
        {
            var item = LHSXCollection.FirstOrDefault(s => s.guid == LHSXSelectedItem.guid);
            if (item == null)
                return;

            item.srdy = SRDYSelectedItem.Value;
            item.fzcy = FZCSSelectedItem.Name;
            item.pdfw = getCSSXXName();
            item.cjkt = Iscj;
            item.dzzzTimeUnit = (RepeatUnitEnum)DZZXUnitSelectedIndex;
            item.dzzx = DZZX + "-" + FunExt.GetRepeatUnitName(DZZXUnitSelectedIndex);

            item.gscTimeUnit = (TimeUnitEnum)GSCUnitSelectedIndex;
            item.gsc = GSC + "-" + FunExt.GetTimeUnitName(GSCUnitSelectedIndex);

            item.kscTimeUnit = (TimeUnitEnum)KSCUnitSelectedIndex;
            item.ksc = KSC + "-" + FunExt.GetTimeUnitName(KSCUnitSelectedIndex);
        }

        private void DeleteLHSX()
        {
            var dialog = MsgBox.QuestionShow("确认删除？");
            if (dialog == MsgBoxResult.OK)
            {
                if (LHSXSelectedItem != null)
                {
                    LHSXCollection.Remove(LHSXSelectedItem);
                }
            }
            LHSXSelectedItem = null;
        }

        private void ResetLHSX()
        {
            var dialog = MsgBox.QuestionShow("确认重置老化顺序？");
            if (dialog == MsgBoxResult.OK)
            {
                LHSXCollection.Clear();
            }
        }

        private void SelectedRFDY()
        {
            var item = SRDYCollection.FirstOrDefault(s => s.Value == LHSXSelectedItem.srdy);
            SRDYSelectedItem = item;
        }

        private void SelectedFZCS()
        {
            var item = FZCSCollection.FirstOrDefault(s => s.Name == LHSXSelectedItem.fzcy);
            FZCSSelectedItem = item;
        }

        private void SelectedCSSXX()
        {
            if (LHSXSelectedItem.pdfw == FunExt.GetCSSXX().First())
                PDFWSelectedIndex = 0;
            if (LHSXSelectedItem.pdfw == FunExt.GetCSSXX().Last())
                PDFWSelectedIndex = 1;
        }

        private string getCSSXXName()
        {
            if (PDFWSelectedIndex == 0)
                return FunExt.GetCSSXX().First();
            else
                return FunExt.GetCSSXX().Last();
        }


    }
}
