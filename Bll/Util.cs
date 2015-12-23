using System;
using System.Drawing;
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

        public static int GetAppId()
        {
            return 5199712;
        }
        public static string GetTelephone()
        {
            return "89999896178";
        }
        public static string GetPassword()
        {
            return "Ivavur2788";
        }
    }
}
