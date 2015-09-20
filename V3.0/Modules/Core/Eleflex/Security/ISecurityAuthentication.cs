namespace Eleflex
{
    /// <summary>
    /// Represents an object that provides authentication security in the application.
    /// </summary>
    public partial interface ISecurityAuthentication
    {
        /// <summary>
        /// Authenticate a credential to return the security user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IResponseItem<ISecurityUser> AuthenticateSecurityCredential(IRequestItem<ISecurityCredential> request);
    }
}
