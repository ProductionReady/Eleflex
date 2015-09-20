namespace Eleflex
{
    /// <summary>
    /// Represents an object containing crypto key information.
    /// </summary>
    public partial class SecurityCryptoKey : ISecurityCryptoKey
    {
        /// <summary>
        /// The key.
        /// </summary>
        public virtual byte[] Key { get; set; }

        /// <summary>
        /// THe initialization vector.
        /// </summary>
        public virtual byte[] IV { get; set; }
    }
}
