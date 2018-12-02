using BITools.Charts;
using BITools.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BITools.DataManager
{
    /// <summary>
    /// 区间数据查询
    /// </summary>
    public partial class TCRecordDataWindow : BaseWindow
    {
        public TCRecordDataWindow()
        {
            InitializeComponent();
            this.DataContext = new LiveDataViewModel();

            this.vChart.DataContext = new VoltageChartViewModel();
            this.aChart.DataContext = new AmpereChartViewModel();
        }
    }
}
