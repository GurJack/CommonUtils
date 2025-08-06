using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;

namespace CommonUtils.Helpers
{
    /// <summary>
    /// Image helper
    /// </summary>
    public static class ImageHelper
    {

        private static readonly ImageFormatConverter _imageFormatConverter = new ImageFormatConverter();


        private static readonly SHA1CryptoServiceProvider sha1Provider = new SHA1CryptoServiceProvider();

        /// <summary>
        /// Compute hash byte array corresponding to selected iamge
        /// </summary>
        public static byte[] ComputeHash(byte[] fromWhat)
        {
            return sha1Provider.ComputeHash(fromWhat);
        }

        private static Size GetProportionalSize(Size szMax, Size szReal)
        {
            int nWidth;
            int nHeight;
            double sMaxRatio;
            double sRealRatio;

            if (szMax.Width < 1 || szMax.Height < 1 || szReal.Width < 1 || szReal.Height < 1)
                return Size.Empty;

            sMaxRatio = (double)szMax.Width / (double)szMax.Height;
            sRealRatio = (double)szReal.Width / (double)szReal.Height;

            if (sMaxRatio < sRealRatio)
            {
                nWidth = Math.Min(szMax.Width, szReal.Width);
                nHeight = (int)Math.Round(nWidth / sRealRatio);
            }
            else
            {
                nHeight = Math.Min(szMax.Height, szReal.Height);
                nWidth = (int)Math.Round(nHeight * sRealRatio);
            }

            return new Size(nWidth, nHeight);
        }


        /// <summary>
        /// Get image format as string
        /// </summary>
        public static string GetImageFormat(Image image)
        {
            return _imageFormatConverter.ConvertToString(image.RawFormat);
        }


        /// <summary>
        /// Generate smaller preview from sourceImage
        /// </summary>
        [Obsolete("Bad quality image")]
        public static Image ProcessingPreviewFoto(Image sourceImage, int newSize = 150)
        {
            int width;
            int height;

            if (sourceImage.Height > sourceImage.Width)
            {
                height = newSize;
                width = (sourceImage.Width * newSize / sourceImage.Height);

                if (width == 0) width = 1;
            }
            else
            {
                width = newSize;
                height = sourceImage.Height * newSize / sourceImage.Width;

                if (height == 0) height = 1;
            }

            //  Image prevImage = sourceImage.GetThumbnailImage(width, height, null, IntPtr.Zero); ;     
            Image prevImage = ResizeImage(sourceImage, width, height, true);
            sourceImage.Dispose();

            return prevImage;
        }


        /// <summary>
        /// Resize image
        /// </summary>
        [Obsolete("Bad quality image")]
        public static Image ResizeImage(Image ImageToResize, int newWidth, int maxHeight, bool onlyResizeIfWider = true, bool needDispose = true)
        {
            // Prevent using images internal thumbnail
            ImageToResize.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
            ImageToResize.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

            if (onlyResizeIfWider)
            {
                if (ImageToResize.Width <= newWidth)
                {
                    newWidth = ImageToResize.Width;
                }
            }

            int newHeight = ImageToResize.Height * newWidth / ImageToResize.Width;
            if (newHeight > maxHeight)
            {
                newWidth = ImageToResize.Width * maxHeight / ImageToResize.Height;
                newHeight = maxHeight;
            }

            Image resizedImage;
            if (newWidth > 100 || newHeight > 100) //GetThumbnailImage is faster but loose quality on big images
            {
                resizedImage = new Bitmap(newWidth, newHeight);
                using (Graphics gr = Graphics.FromImage(resizedImage))
                {
                    //      gr.SmoothingMode = SmoothingMode.HighQuality;
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    //      gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    gr.DrawImage(ImageToResize, 0, 0, newWidth, newHeight);
                }
            }
            else
            {
                resizedImage = ImageToResize.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);
            }


            if (needDispose)
            {
                ImageToResize.Dispose();
            }


            return resizedImage;
        }

        /// <summary>
        /// Generate smaller preview from sourceImage
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image GetResizeImage(Image image, int width, int height)
        {            
            Size szMax = new System.Drawing.Size(width, height);
            Size sz = GetProportionalSize(szMax, image.Size);
            
            Bitmap bmpResized = new Bitmap(sz.Width, sz.Height);
            using (Graphics g = Graphics.FromImage(bmpResized))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(
                    image,
                    new Rectangle(Point.Empty, sz),
                    new Rectangle(Point.Empty, image.Size),
                    GraphicsUnit.Pixel);
            }
            image.Dispose();

            return bmpResized;
        }

        /// <summary>
        /// Convert Image to byte array
        /// </summary>
        public static byte[] ImageToByteArray(Image imageIn, ImageFormat imageFormat = null)
        {
            using (var memStream = new MemoryStream())
            {
                if (imageFormat == null)
                    imageFormat = imageIn.RawFormat;

                if (imageFormat.Equals(ImageFormat.MemoryBmp))
                    imageFormat = ImageFormat.Jpeg;

                imageIn.Save(memStream, imageFormat);
                return memStream.ToArray();
            }
        }

        /// <summary>
        /// Convert byte array to system.drawing.image
        /// </summary>
        public static Image ByteArrayToImage(byte[] fileBytes)
        {
            if (fileBytes == null)
                return null;

            using (var memStream = new MemoryStream(fileBytes))
            {
                using (var tmpImage = Image.FromStream(memStream))
                {
                    return new Bitmap(tmpImage);
                }
            }

            //Image returnMe;
            //using (var ms = new MemoryStream(fileBytes))
            //{
            //    var img = Image.FromStream(ms);

            //    returnMe = (Image) img.Clone();

            //    img = null;

            //    ms.Close();
            //}

            //return returnMe;
        }
    }
}
