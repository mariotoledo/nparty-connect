using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CampeonatosNParty.Helpers
{
    public class ImageHelper
    {
        public static bool IsImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return false;
            }

            if (file.ContentLength < 1024 || file.ContentLength > 10485760)
            {
                return false;
            }

            try
            {
                var allowedFormats = new[] 
                { 
                    ImageFormat.Jpeg, 
                    ImageFormat.Png
                };

                using (var img = Image.FromStream(file.InputStream))
                {
                    return allowedFormats.Contains(img.RawFormat);
                }
            }
            catch { }
            return false;
        }

        public static bool IsEventImage(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return false;
            }

            if (file.ContentLength < 1024 || file.ContentLength > 10485760)
            {
                return false;
            }

            try
            {
                var allowedFormats = new[] 
                { 
                    ImageFormat.Png
                };

                using (var img = Image.FromStream(file.InputStream))
                {
                    return allowedFormats.Contains(img.RawFormat);
                }
            }
            catch { }
            return false;
        }

        public static Stream ResizeAndCropStream(int imageSize, Stream filePath)
        {
            var imgToResize = Image.FromStream(filePath);

            int newWidth = imgToResize.Width > imgToResize.Height ? imgToResize.Width * imageSize / imgToResize.Height : imageSize;
            int newHeight = imgToResize.Width < imgToResize.Height ? imgToResize.Height * imageSize / imgToResize.Width : imageSize;
            int offsetX = newWidth > imageSize ? - ((newWidth - imageSize) / 2) : 0;
            int offsetY = newHeight > imageSize ? - ((newHeight - imageSize) / 2) : 0;

            Bitmap b = new Bitmap(imageSize, imageSize);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, offsetX, offsetY, newWidth, newHeight);
            g.Dispose();

            return ConvertToStream((Image)b, ImageFormat.Jpeg);
        }

        public static Stream ResizeAndCropStream(int width, int height, Stream filePath)
        {
            var imgToResize = Image.FromStream(filePath);

            int newWidth = imgToResize.Width, newHeight = imgToResize.Height, offsetX = 0, offsetY = 0;

            if (imgToResize.Width > width)
            {
                newWidth = width;
                newHeight = imgToResize.Height * newWidth / imgToResize.Width;
            }
            else if (imgToResize.Height > height)
            {
                newHeight = height;
                newWidth = imgToResize.Width * newHeight / imgToResize.Height; ;
            }

            if (newHeight > height)
            {
                newWidth = newWidth * height / newHeight;
                newHeight = height;                
            }

            offsetX = width / 2 - newWidth / 2;
            offsetY = height / 2 - newHeight / 2;

            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, offsetX, offsetY, newWidth, newHeight);
            g.Dispose();

            return ConvertToStream((Image)b, ImageFormat.Png);
        }

        public static Stream ConvertToStream(Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }

        public static Stream CreateBigEventImage(Stream logoStream)
        {
            return CreateEventImage(logoStream, 1000, 250, 1);
        }

        public static Stream CreateSmallEventImage(Stream logoStream)
        {
            return CreateEventImage(logoStream, 450, 450, 2);
        }

        public static Stream CreateEventImage(Stream logoStream, int width, int height, int type)
        {
            Stream redizedLogo = ResizeAndCropStream(width, height, logoStream);
            Image background = type == 1 ? AdminConnect.Properties.Resources.padraoBig : AdminConnect.Properties.Resources.padraoSmall;

            Graphics g = System.Drawing.Graphics.FromImage(background);
            Image logo = Image.FromStream(redizedLogo);

            Bitmap TransparentLogo = new Bitmap(logo.Width, logo.Height);
            Graphics TGraphics = Graphics.FromImage(TransparentLogo);
            ColorMatrix ColorMatrix = new ColorMatrix();
            ColorMatrix.Matrix33 = 1F;
            ImageAttributes ImgAttributes = new ImageAttributes();
            ImgAttributes.SetColorMatrix(ColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            TGraphics.DrawImage(logo, 0, 0);// new Rectangle(0, 0, TransparentLogo.Width, TransparentLogo.Height), 0, 0, TransparentLogo.Width, TransparentLogo.Height, GraphicsUnit.Pixel, ImgAttributes);
            TGraphics.Dispose();
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(TransparentLogo, (background.Width / 2) - (logo.Width / 2), (background.Height / 2) - (logo.Height / 2), logo.Width, logo.Height);

            return ConvertToStream(background, ImageFormat.Png);
        }
    }
}