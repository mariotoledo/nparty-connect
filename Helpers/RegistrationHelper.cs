using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CampeonatosNParty.Helpers
{
    public class RegisterHelper
    {
        public static string GetRandWord(int length)
        {
            string checkCode = string.Empty;
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                char code;
                int number = random.Next();
                if ((number % 3) == 0)
                {
                    code = (char)(0x30 + ((ushort)(number % 10)));
                }
                else
                {
                    code = (char)(0x41 + ((ushort)(number % 0x1a)));
                }
                checkCode = checkCode + code.ToString();
            }
            return checkCode;
        }

        private static string Encrypt(string original)
        {
            return EixoX.CryptoHelper.Sha1Hash(original);
        }

        public static string GetEncryptedPassword(string original)
        {
            string randWord = GetRandWord(5);
            return "sha1$" + randWord + "$" + Encrypt(randWord + original).ToLower();
        }

        public static bool CheckValidPassword(string passwordToCheck, string passwordStored)
        {
            if (passwordToCheck.IndexOf('$') > 0)
            {
                string[] pwd = passwordToCheck.Split('$');
                string pwdHash = EixoX.CryptoHelper.Sha1Hash(pwd[1] + passwordStored).ToLower();
                return pwdHash.Equals(pwd[2], StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public static Bitmap GenerateImage(int Width, int Height, string Phrase)
        {
            Bitmap CaptchaImg = new Bitmap(Width, Height);
            Random Randomizer = new Random();
            Graphics Graphic = Graphics.FromImage(CaptchaImg);
            Graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            Graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Graphic.FillRectangle(new SolidBrush(Color.FromArgb(Randomizer.Next(0, 100),
                Randomizer.Next(0, 100), Randomizer.Next(0, 100))), 0, 0, Width, Height);
            Graphic.RotateTransform(Randomizer.Next(-15, 5));
            Graphic.DrawString(Phrase, new Font("Verdana", 30),
                new SolidBrush(Color.FromArgb(Randomizer.Next(150, 255),
                Randomizer.Next(150, 255), Randomizer.Next(150, 255))), 15, 35);
            Graphic.Flush();
            return CaptchaImg;
        }
    }
}