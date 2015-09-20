using System;
using System.Linq;
using System.Security.Cryptography;
using System.IO;

namespace Eleflex
{
    /// <summary>
    /// Represents an object defining a cryptography service used to encrypt and decrypt values.
    /// </summary>
    public partial class SecurityCryptoService : ISecurityCryptoService
    {

        /// <summary>
        /// Get a random crypto key.
        /// </summary>
        /// <returns></returns>
        public virtual ISecurityCryptoKey CreateRandomCryptoKey()
        {
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                return new SecurityCryptoKey() { Key = aesAlg.Key, IV = aesAlg.IV };
            }
        }

        /// <summary>
        /// Enrypt data with password.
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

                ISecurityCryptoKey key = new SecurityCryptoKey();
                key.Key = passBytes.Take(32).ToArray();
                key.IV = passBytes.Take(16).ToArray();

                return Convert.ToBase64String(Encrypt(System.Text.Encoding.UTF8.GetBytes(data), key));
            }
            catch { return null; }
        }

        /// <summary>
        /// Encrypt data with a key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cryptoKey"></param>
        /// <returns></returns>
        public virtual byte[] Encrypt(byte[] data, ISecurityCryptoKey cryptoKey)
        {
            try
            {
                // Check arguments. 
                if (data == null || data.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (cryptoKey.Key == null || cryptoKey.Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (cryptoKey.IV == null || cryptoKey.IV.Length <= 0)
                    throw new ArgumentNullException("IV");
                // Create an AesCryptoServiceProvider object with the specified key and IV. 
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = cryptoKey.Key;
                    aesAlg.IV = cryptoKey.IV;

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
        /// Decrypt data with password.
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
                ISecurityCryptoKey key = new SecurityCryptoKey();
                key.Key = passBytes.Take(32).ToArray();
                key.IV = passBytes.Take(16).ToArray();
                byte[] output = Decrypt(dataBytes, key);
                return System.Text.Encoding.UTF8.GetString(output);
            }
            catch { return null; }
        }

        /// <summary>
        /// Decrypt data with a key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cryptoKey"></param>
        /// <returns></returns>
        public virtual byte[] Decrypt(byte[] data, ISecurityCryptoKey cryptoKey)
        {
            try
            {
                // Check arguments. 
                if (data == null || data.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (cryptoKey.Key == null || cryptoKey.Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (cryptoKey.IV == null || cryptoKey.IV.Length <= 0)
                    throw new ArgumentNullException("IV");
                // Create an AesCryptoServiceProvider object with the specified key and IV. 
                using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
                {
                    aesAlg.Key = cryptoKey.Key;
                    aesAlg.IV = cryptoKey.IV;

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

        
    }
}
