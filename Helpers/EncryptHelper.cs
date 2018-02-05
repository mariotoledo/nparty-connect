using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace CampeonatosNParty.Helpers
{
    public class EncryptHelper
    {
        public static string Encrypt(int decimalNumber)
        {
            byte[] byte_data = new byte[decimalNumber.ToString().Length];
            byte_data = System.Text.Encoding.UTF8.GetBytes(decimalNumber.ToString());
            string encodedData = Convert.ToBase64String(byte_data);
            return encodedData;
        }

        public static int Decrypt(string encryptedText)
        {
            try
            {

                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] byte_data = Convert.FromBase64String(encryptedText);
                int charCount = utf8Decode.GetCharCount(byte_data, 0, byte_data.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(byte_data, 0, byte_data.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return Int32.Parse(result);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
