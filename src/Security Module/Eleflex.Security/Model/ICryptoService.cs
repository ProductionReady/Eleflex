using System;

namespace Eleflex.Security
{
    /// <summary>
    /// Interface defining a cryptography service used to encrypt and decrypt values.
    /// </summary>
    public interface ICryptoService
    {

        /// <summary>
        /// Get a random crypto key.
        /// </summary>
        /// <returns></returns>
        CryptoKey CreateRandomCryptoKey();

        /// <summary>
        /// Encrypt data with password.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string Encrypt(string data, string password);

        /// <summary>
        /// Encrypt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] data, byte[] key, byte[] iv);

        /// <summary>
        /// Decrypt data with password.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string Decrypt(string data, string password);

        /// <summary>
        /// Decrypt.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] data, byte[] key, byte[] iv);
    }
}
