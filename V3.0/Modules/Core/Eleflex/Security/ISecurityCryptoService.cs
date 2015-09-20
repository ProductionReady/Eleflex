namespace Eleflex
{
    /// <summary>
    /// Represents an object defining a cryptography service used to encrypt and decrypt values.
    /// </summary>
    public partial interface ISecurityCryptoService
    {

        /// <summary>
        /// Get a random crypto key.
        /// </summary>
        /// <returns></returns>
        ISecurityCryptoKey CreateRandomCryptoKey();

        /// <summary>
        /// Encrypt data with password.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string Encrypt(string data, string password);

        /// <summary>
        /// Encrypt data with a key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cryptoKey"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] data, ISecurityCryptoKey cryptoKey);

        /// <summary>
        /// Decrypt data with password.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string Decrypt(string data, string password);

        /// <summary>
        /// Decrypt data with a key.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="cryptoKey"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] data, ISecurityCryptoKey cryptoKey);
    }
}
