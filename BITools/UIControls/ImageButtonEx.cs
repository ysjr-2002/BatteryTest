using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BITools.UIControls
{
    public class ImageButtonEx : Button
    {
        public static readonly DependencyProperty NormalImageProperty;

        public static readonly DependencyProperty HoverImageProperty;

        public static readonly DependencyProperty DisableImageProperty;

        public ImageButtonEx()
        {
            //DefaultStyleKey = typeof(ImageButtonEx);
        }

        static ImageButtonEx()
        {
            NormalImageProperty = DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(ImageButtonEx));
            HoverImageProperty = DependencyProperty.Register("HoverImage", typeof(ImageSource), typeof(ImageButtonEx));
            DisableImageProperty = DependencyProperty.Register("DisableImage", typeof(ImageSource), typeof(ImageButtonEx));

            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButtonEx), new FrameworkPropertyMetadata(typeof(ImageButtonEx)));
        }

        /// <summary>
        /// Normal Image
        /// </summary>
        public ImageSource NormalImage
        {
            get { return (ImageSource)GetValue(NormalImageProperty); }
            set { SetValue(NormalImageProperty, value); }
        }

        /// <summary>
        /// Hover image
        /// </summary>
        public ImageSource HoverImage
        {
            get { return (ImageSource)GetValue(HoverImageProperty); }
            set { SetValue(HoverImageProperty, value); }
        }

        /// <summary>
        /// disable image
        /// </summary>
        public ImageSource DisableImage
        {
            get { return (ImageSource)GetValue(DisableImageProperty); }
            set { SetValue(DisableImageProperty, value); }
        }
    }
}
