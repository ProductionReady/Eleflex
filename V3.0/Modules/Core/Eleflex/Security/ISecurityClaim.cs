namespace Eleflex
{
    /// <summary>
    /// Represents an object that contains securit claim information.
    /// </summary>
    public partial interface ISecurityClaim
    {
        /// <summary>
        /// The name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The issuer.
        /// </summary>
        string Issuer { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        string Value { get; set; }
    }
}
