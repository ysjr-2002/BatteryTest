using Common.NotifyBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BITools.ViewModel
{
    /// <summary>
    /// 备用
    /// </summary>
    class MyState : PropertyNotifyObject
    {
        //脱机
        public bool Istj
        {
            get { return this.GetValue(c => c.Istj); }
            set { this.SetValue(c => c.Istj, value); }
        }

        //下载参数中
        public bool Isxzczz
        {
            get { return this.GetValue(c => c.Isxzczz); }
            set { this.SetValue(c => c.Isxzczz, value); }
        }

        //停止拉载
        public bool Istzlz
        {
            get { return this.GetValue(c => c.Istzlz); }
            set { this.SetValue(c => c.Istzlz, value); }
        }

        //拉载异常
        public bool Islzyc
        {
            get { return this.GetValue(c => c.Islzyc); }
            set { this.SetValue(c => c.Islzyc, value); }
        }

        //无产品
        public bool Iswcp
        {
            get { return this.GetValue(c => c.Iswcp); }
            set { this.SetValue(c => c.Iswcp, value); }
        }

        //欠压
        public bool Isqy
        {
            get { return this.GetValue(c => c.Isqy); }
            set { this.SetValue(c => c.Isqy, value); }
        }

        //欠流
        public bool Isql
        {
            get { return this.GetValue(c => c.Isql); }
            set { this.SetValue(c => c.Isql, value); }
        }

        //过压
        public bool Isgy
        {
            get { return this.GetValue(c => c.Isgy); }
            set { this.SetValue(c => c.Isgy, value); }
        }

        //过流
        public bool Isgl
        {
            get { return this.GetValue(c => c.Isgl); }
            set { this.SetValue(c => c.Isgl, value); }
        }

        //无输出
        public bool Iswsc
        {
            get { return this.GetValue(c => c.Iswsc); }
            set { this.SetValue(c => c.Iswsc, value); }
        }

        //合格
        public bool Ishg
        {
            get { return this.GetValue(c => c.Ishg); }
            set { this.SetValue(c => c.Ishg, value); }
        }

        //负载保护
        public bool Isfzbh
        {
            get { return this.GetValue(c => c.Isfzbh); }
            set { this.SetValue(c => c.Isfzbh, value); }
        }
    }
}
