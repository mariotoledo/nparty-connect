using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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

        public static Stream ConvertToStream(Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }

        public static string GetSmashCharacterImage(int characterId, int clothId)
        {
            string characterIdString = characterId <= 0 || characterId > 49 ? "0" : characterId.ToString();
            string clothIdString = characterId <= 0 || characterId > 49 ? "0" : clothId.ToString();

            return "~/Static/img/smashBrosIcons/" + characterIdString + "/" + clothIdString + ".png";
        }
    }
}