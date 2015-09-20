namespace Eleflex
{
    /// <summary>
    /// Represents an object that contains securit claim information.
    /// </summary>
    public partial class SecurityClaim : ISecurityClaim
    {

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The issuer.
        /// </summary>
        public virtual string Issuer { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        public virtual string Value { get; set; }

    }
}
