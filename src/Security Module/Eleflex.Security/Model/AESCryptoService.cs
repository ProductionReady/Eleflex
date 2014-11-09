using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Eleflex.Security
{
    /// <summary>
    /// Defines a crypto class for the AES cryto provider
    /// </summary>
    public partial class AESCryptoService : ICryptoService
    {

        /// <summary>
        /// Get a random crypto key.
        /// </summary>
        /// <returns></returns>
        public virtual CryptoKey CreateRandomCryptoKey()
        {
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                return new CryptoKey()
                {
                    Key = aesAlg.Key,
                    IV = aesAlg.IV
                };
            }
        }

        /// <summary>
        /// Enrypt data with the password.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual string Encrypt(string data, string password)
        {
            try
            {
                string validLengthPass = password;
                if (validLengthPass.Length < 32)
                    validLengthPass = validLengthPass.PadRight(32, 'z');
                byte[] passBytes = System.Text.Encoding.UTF8.GetBytes(validLengthPass);
                byte[] key = passBytes.Take(32).ToArray();
                byte[] iv = passBytes.Take(16).ToArray();
                return Convert.ToBase64String(EncryptStringToBytes_Aes(data, key, iv));
            }
            catch { return null; }
        }

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public virtual byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            try
            {
                // Check arguments. 
                if (data == null || data.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (key == null || key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (iv == null || iv.Length <= 0)
                    throw new ArgumentNullException("IV");
                // Create an AesCryptoServiceProvider object 
                // with the specified key and IV. 
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for encryption. 
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(data, 0, data.Length);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
            catch
            { return null; }
        }


        /// <summary>
        /// Decrypt data with the password.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public virtual string Decrypt(string data, string password)
        {
            try
            {
                string validLengthPass = password;
                if (validLengthPass.Length < 32)
                    validLengthPass = validLengthPass.PadRight(32, 'z');
                byte[] dataBytes = Convert.FromBase64String(data);
                byte[] passBytes = System.Text.Encoding.UTF8.GetBytes(validLengthPass);
                byte[] key = passBytes.Take(32).ToArray();
                byte[] iv = passBytes.Take(16).ToArray();
                return DecryptStringFromBytes_Aes(dataBytes, key, iv);
            }
            catch { return null; }
        }

        /// <summary>
        /// Decrypt.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public virtual byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            try
            { 
                // Check arguments. 
                if (data == null || data.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (key == null || key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (iv == null || iv.Length <= 0)
                    throw new ArgumentNullException("IV");
                // Create an AesCryptoServiceProvider object 
                // with the specified key and IV. 
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    // Create the streams used for decryption. 
                    using (MemoryStream msDecrypt = new MemoryStream())
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                        {
                            csDecrypt.Write(data, 0, data.Length);
                        }
                        return msDecrypt.ToArray();
                    }
                }
            }
            catch { return null; }
        }


        /// <summary>
        /// Encrypt a string. Code pulled from Microsoft website example.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        /// <summary>
        /// Decrypt a string. From Microsoft website example.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }

    }
}