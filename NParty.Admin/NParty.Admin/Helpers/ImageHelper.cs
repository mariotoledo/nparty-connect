using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace NParty.Admin.Helpers
{
    public class ImageHelper
    {
        public static string[] ImageExtensions = new string[] { "jpg", "gif", "png", "jpeg", "bmp", "tiff", "tif" };

        public static readonly Dictionary<ImageSize, Size> Sizes = new Dictionary<ImageSize, Size>()
        {
            {ImageSize.Small, new Size(160, 160)},
            {ImageSize.Medium, new Size(320, 320)},
            {ImageSize.Big, new Size(640, 640)}
        };

        public static readonly Dictionary<ImageSize, string> ImageSizeAcronym = new Dictionary<ImageSize, string>()
        {
            {ImageSize.Small, "SML"},
            {ImageSize.OriginalImage, "ORG"},
            {ImageSize.Medium, "MED"},
            {ImageSize.Big, "BIG"}
        };

        public static readonly Dictionary<ImageBlobFormat, string> ImageFormatExtension = new Dictionary<ImageBlobFormat, string>()
        {
            {ImageBlobFormat.Bmp, ".bmp"},
            {ImageBlobFormat.Gif, ".gif"},
            {ImageBlobFormat.Jpeg, ".jpg"},
            {ImageBlobFormat.Png, ".png"},
            {ImageBlobFormat.Tif, ".tif"}
        };

        public static Image ResizeImage(Image sourceImage, ImageSize imageSize)
        {
            return ResizeImage(sourceImage, Sizes[imageSize].Width, Sizes[imageSize].Height);
        }

        public static Bitmap ResizeImage(Image source, Size size)
        {
            return ResizeImage(source, size.Width, size.Height);
        }

        /// <summary>
        /// Resizes a image to the size you want
        /// </summary>
        /// <param name="sourceImage">Original image to be resized</param>
        /// <param name="width">Width to resize</param>
        /// <param name="height">Height to resize</param>
        /// <param name="imageFormat">image format</param>
        /// <returns>The resized image</returns>
        public static Bitmap ResizeImage(Image sourceImage, int width, int height, ImageBlobFormat imageFormat)
        {
            float resizePercent = 0;
            float percentWidth = ((float)width / (float)sourceImage.Width);
            float percentHeight = ((float)height / (float)sourceImage.Height);
            if (percentWidth > percentHeight)
                resizePercent = percentWidth;
            else
                resizePercent = percentHeight;
            Bitmap resized = new Bitmap((int)(sourceImage.Width * resizePercent), (int)(sourceImage.Height * resizePercent));
            Bitmap b = new Bitmap(width, height);
            Rectangle cropArea;

            System.Drawing.Graphics graphic = Graphics.FromImage((Image)resized);
            graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            if (imageFormat == ImageBlobFormat.Jpeg)
                graphic.Clear(Color.White);
            graphic.DrawImage(sourceImage, 0, 0, resized.Width, resized.Height);
            graphic.Dispose();

            if (resized.Width > resized.Height)
                cropArea = new Rectangle((resized.Width - resized.Height) / 2, 0, resized.Height, resized.Height);
            else
                cropArea = new Rectangle(0, (resized.Height - resized.Width) / 2, resized.Width, resized.Width);

            b = CropBitmap(resized, cropArea);

            return b;
        }

        public static Bitmap ResizeImage(Image sourceImage, int width, int height)
        {
            return ResizeImage(sourceImage, width, height, ImageBlobFormat.Jpeg);
        }

        /// <summary>
        /// Returns a MemoryStream from an Image
        /// </summary>
        /// <param name="image">The original image</param>
        /// <returns>A MemoryStrem from an image</returns>
        public static MemoryStream GetStream(Image image, ImageBlobFormat imageFormat, int quality)
        {
            MemoryStream ms = new MemoryStream(image.Width * image.Height * 4);

            switch (imageFormat)
            {
                case ImageBlobFormat.Bmp:
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case ImageBlobFormat.Gif:
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case ImageBlobFormat.Jpeg:

                    System.Drawing.Imaging.EncoderParameters eps = new System.Drawing.Imaging.EncoderParameters(1);
                    eps.Param[0] = new System.Drawing.Imaging.EncoderParameter(
                        System.Drawing.Imaging.Encoder.Quality,
                        (long)quality);

                    image.Save(ms, GetEncoderInfo("image/jpeg"), eps);

                    break;
                case ImageBlobFormat.Png:
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }

            return ms;
        }


        /// <summary>
        /// Crops an Image to wanted size
        /// </summary>
        /// <param name="img">The original image to be cropped</param>
        /// <param name="cropArea">The area to crop the image</param>
        /// <returns>The cropped image</returns>
        public static Bitmap CropBitmap(Bitmap img, Rectangle cropArea)
        {
            Bitmap bmpCrop = img.Clone(cropArea, img.PixelFormat);
            return bmpCrop;
        }

        public static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            System.Drawing.Imaging.ImageCodecInfo[] encoders;
            encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (mimeType.Equals(encoders[j].MimeType, StringComparison.OrdinalIgnoreCase))
                    return encoders[j];
            }
            return null;
        }
    }


    public enum ImageBlobFormat
    {
        Jpeg,
        Gif,
        Png,
        Bmp,
        Tif
    }

    public enum ImageSize
    {
        OriginalImage = 1,
        Big = 2,
        Medium = 4,
        Small = 8
    }
}