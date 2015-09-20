namespace Eleflex
{
    /// <summary>
    /// Represents an object containing crypto key information.
    /// </summary>
    public partial interface ISecurityCryptoKey
    {
        /// <summary>
        /// The key.
        /// </summary>
        byte[] Key { get; set; }

        /// <summary>
        /// THe initialization vector.
        /// </summary>
        byte[] IV { get; set; }
    }
}
