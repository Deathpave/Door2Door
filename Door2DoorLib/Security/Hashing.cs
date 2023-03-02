﻿using System.Security.Cryptography;
using System.Text;

namespace Door2DoorLib.Security
{
    internal class Hashing
    {
        #region Sha 256 Hash
        public string Sha256Hash(string input)
        {
            // Check for null or empty input
            if (string.IsNullOrEmpty(input))
                throw new Exception("Input string was empty");

            // Convert input to bytes
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            SHA256 sha256 = SHA256.Create();
            // Return hashed input
            return Convert.ToBase64String(sha256.ComputeHash(inputBytes, 0, inputBytes.Length));
        }
        #endregion
    }
}