using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CommonWPF.Extensions
{
    /// <summary>
    /// Image extensions
    /// </summary>
    public static class ImageExtension
    {
        /// <summary>
        /// Convert Icon to ImageSource
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        public static ImageSource ToImageSource(this Icon icon)
        {
            if (icon == null) return null;

            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }

        /// <summary>
        /// Convert Bitmap to ImageSource
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapSource ToImageSource(this System.Drawing.Bitmap bitmap)
        {
            if (bitmap == null)
                return null;

            var hBitmap = bitmap.GetHbitmap();
            var sizeOptions = BitmapSizeOptions.FromEmptyOptions();
            var bms = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap,
                IntPtr.Zero, Int32Rect.Empty, sizeOptions);
            bms.Freeze();

            // Free memory:
            DeleteObject(hBitmap);

            bitmap.Dispose();

            return bms;
        }

        /// <summary>
        /// Convert BitmapSource to Image.
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static BitmapSource ToImageSource(this System.Drawing.Image image)
        {
            if (image == null) return null;

            return ToImageSource(new Bitmap(image));
        }

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// Convert BitmapSource to ImageSource
        /// </summary>
        /// <param name="srs"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap ToBitmap(this BitmapSource srs)
        {
            var width = srs.PixelWidth;
            var height = srs.PixelHeight;
            var stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
            var ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(height * stride);
                srs.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
                using (var btm = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, ptr))
                {
                    // Clone the bitmap so that we can dispose it and
                    // release the unmanaged memory at ptr
                    return new System.Drawing.Bitmap(btm);
                }
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    Marshal.FreeHGlobal(ptr);
            }
        }
    }
}