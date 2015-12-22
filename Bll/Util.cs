using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FoolGame.Bll
{
    class Util
    {
        public static ImageSource ConvertToImageSource(Bitmap bitmap)
        {
            return Imaging.CreateBitmapSourceFromHBitmap(
                    bitmap.GetHbitmap(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
        }

        public static ImageBrush ConvertToImageBrush(Bitmap bitmap)
        {
            return new ImageBrush
            {
                ImageSource = ConvertToImageSource(bitmap)
            };
        }
    }
}
