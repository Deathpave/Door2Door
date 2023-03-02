using System.Security.Cryptography;
using System.Text;

namespace Door2DoorLib.Security
{
    internal class Encryption
    {
        #region Encrypt String
        public string EncryptString(string input, string encodingPassword)
        {
            // Check for null or empty inputs
            if (string.IsNullOrEmpty(input))
                throw new Exception("Input string was empty");
            if (string.IsNullOrEmpty(encodingPassword))
                throw new Exception("Encoding password was empty");

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
