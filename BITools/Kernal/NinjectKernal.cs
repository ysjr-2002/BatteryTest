using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using BITools.ViewModel;
using BITools.Core;

namespace LL.SenicSpot.Gate.Kernal
{
    public class NinjectKernal
    {
        static NinjectKernal _kernal = null;
        static object sync = new object();

        private NinjectKernal()
        {
        }

        public static NinjectKernal Instance
        {
            get
            {
                if (_kernal == null)
                {
                    lock (sync)
                    {
                        if (_kernal == null)
                            _kernal = new NinjectKernal();
                    }
                }

                return _kernal;
            }
        }

        StandardKernel kernal = new StandardKernel();
        public void Load()
        {
            kernal.Bind<MainViewModel>().ToSelf().InSingletonScope();
            kernal.Bind<Config>().ToSelf().InSingletonScope();

            kernal.Get<Config>().Read();
        }

        public T Get<T>()
        {
            return kernal.Get<T>();
        }
    }
}
