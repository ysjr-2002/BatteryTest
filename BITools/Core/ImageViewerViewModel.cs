using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BITools.Core
{
    public class ImageViewerViewModel
    {
        public ICommand NextCommand { get; set; }
        public ICommand PreviousCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public Action PreviouseHanlder { get; set; }
        public Action NextHanlder { get; set; }

        public ImageViewerViewModel()
        {
            PreviousCommand = new DelegateCommand(this.Previous);
            NextCommand = new DelegateCommand(this.Next);
            CloseWindowCommand = new DelegateCommand<Window>(this.Close);
        }

        private void Previous()
        {
            PreviouseHanlder?.Invoke();
        }

        private void Next()
        {
            NextHanlder?.Invoke();
        }

        private void Close(Window window)
        {
            window?.Close();
        }
    }
}
