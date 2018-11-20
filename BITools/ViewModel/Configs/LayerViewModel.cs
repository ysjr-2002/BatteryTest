using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.NotifyBase;
using System.Collections.ObjectModel;

namespace BITools.ViewModel.Configs
{
    /// <summary>
    /// 层ViewModel
    /// </summary>
    public class LayerViewModel : PropertyNotifyObject
    {
        public LayerViewModel()
        {
            this.UUTList = new ObservableCollection<UUTViewModel>();
            this.IsEnable = true;
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Code
        {
            get { return this.GetValue(c => c.Code); }
            set { this.SetValue(c => c.Code, value); }
        }

        /// <summary>
        /// 层名称
        /// </summary>
        public string Name
        {
            get { return this.GetValue(c => c.Name); }
            set { this.SetValue(c => c.Name, value); }
        }

        /// <summary>
        /// 老化时间
        /// </summary>
        public string LHSJ
        {
            get { return this.GetValue(c => c.LHSJ); }
            set { this.SetValue(c => c.LHSJ, value); }
        }

        public bool IsEnable
        {
            get { return this.GetValue(c => c.IsEnable); }
            set { this.SetValue(c => c.IsEnable, value); }
        }

        /// <summary>
        /// 台车集合
        /// </summary>
        public ObservableCollection<UUTViewModel> UUTList
        {
            get { return this.GetValue(c => c.UUTList); }
            set { this.SetValue(c => c.UUTList, value); }
        }
    }
}
