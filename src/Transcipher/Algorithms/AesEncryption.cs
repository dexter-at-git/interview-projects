using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Transcipher.Algorithms
{
    public class AesEncryption : IEncryptionAlgorithm
    {
        private byte[] _keyBytes = Encoding.UTF8.GetBytes("tR7nR6wZHGjYMCuV");

        public string Encrypt(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            byte[] encrypted;
            byte[] IV;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _keyBytes;

                aesAlg.GenerateIV();
                IV = aesAlg.IV;

                aesAlg.Mode = CipherMode.CBC;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            var combinedIvCt = new byte[IV.Length + encrypted.Length];
            Array.Copy(IV, 0, combinedIvCt, 0, IV.Length);
            Array.Copy(encrypted, 0, combinedIvCt, IV.Length, encrypted.Length);

            return Convert.ToBase64String(combinedIvCt);
        }


        public string Decrypt(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return String.Empty;
            }

            string plaintext = null;
            var cipherTextCombined = Convert.FromBase64String(input);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _keyBytes;

                byte[] IV = new byte[aesAlg.BlockSize / 8];
                byte[] cipherText = new byte[cipherTextCombined.Length - IV.Length];

                Array.Copy(cipherTextCombined, IV, IV.Length);
                Array.Copy(cipherTextCombined, IV.Length, cipherText, 0, cipherText.Length);

                aesAlg.IV = IV;

                aesAlg.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }
    }
}