namespace Eleflex
{
    /// <summary>
    /// Represents an object containing security credential information.
    /// </summary>
    public partial interface ISecurityCredential
    {
        /// <summary>
        /// The username.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// The email.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// The password.
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// The pin.
        /// </summary>
        string Pin { get; set; }

        /// <summary>
        /// The token.
        /// </summary>
        string Token { get; set; }
    }
}
