using Door2DoorLib.Factories;
using System.Security.Cryptography;
using System.Text;

namespace Door2DoorLib.Security
{
    internal class Encryption
    {
        #region Encrypt String
        /// <summary>
        /// Returns encrypted input string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encodingPassword"></param>
        /// <returns></returns>
        public string EncryptString(string input, string encodingPassword)
        {
            // Check for null or empty inputs
            if (string.IsNullOrEmpty(input))
            {
                LogFactory.CreateLog(LogTypes.Console, "Input string was null or empty", MessageTypes.Error).WriteLog();
            }
            if (string.IsNullOrEmpty(encodingPassword))
                LogFactory.CreateLog(LogTypes.Console, "Encoding password was null or empty", MessageTypes.Error).WriteLog();

            // input bytes as salt
            byte[] salt = Encoding.UTF8.GetBytes(input);
            // Key and vector generator
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(encodingPassword, salt);

            // Create the Aes for encryptin
            Aes aes = Aes.Create();
            aes.Key = rfc.GetBytes(32);
            aes.IV = rfc.GetBytes(16);

            // Create streams for encryption
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);

            // Encrypt input as bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            cryptoStream.Write(inputBytes, 0, inputBytes.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();

            // Convert encrypted bytes to base64 string
            string result = Convert.ToBase64String(memoryStream.ToArray());
            return result;
        }
        #endregion
    }
}
