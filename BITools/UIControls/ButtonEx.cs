using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BITools.UIControls
{
    public class ButtonEx : Button
    {
        public static readonly DependencyProperty NormalImageProperty;
        public static readonly DependencyProperty NormalImagePlacementProperty;
        public static readonly DependencyProperty DisableImageProperty;
        static ButtonEx()
        {
            NormalImageProperty = DependencyProperty.Register("NormalImage", typeof(ImageSource), typeof(ButtonEx));

            NormalImagePlacementProperty = DependencyProperty.Register("NormalImagePlacement", typeof(ImagePlacement), typeof(ButtonEx), new PropertyMetadata(ImagePlacement.Left));

            DisableImageProperty = DependencyProperty.Register("DisableImage", typeof(ImageSource), typeof(ButtonEx));

        }


        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(ButtonEx), new PropertyMetadata(16d));

        public ImageSource DisableImage
        {
            get { return (ImageSource)GetValue(DisableImageProperty); }
            set { SetValue(DisableImageProperty, value); }
        }

        public ImageSource NormalImage
        {
            get { return (ImageSource)GetValue(NormalImageProperty); }
            set { SetValue(NormalImageProperty, value); }
        }

        public ImagePlacement NormalImagePlacement
        {
            get { return (ImagePlacement)GetValue(NormalImagePlacementProperty); }
            set { SetValue(NormalImagePlacementProperty, value); }
        }
    }

    public enum ImagePlacement
    {
        Top,
        Left,
        Right
    }
}
