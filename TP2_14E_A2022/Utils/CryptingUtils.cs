using System;
using System.Security.Cryptography;

namespace TP2_14E_A2022.Utils
{
    public static class CryptingUtils
    {
        public static string GetStringSha256Hash(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            using (SHA256 sha = SHA256.Create())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}
