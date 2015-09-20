namespace Eleflex
{
    /// <summary>
    /// Represents an object that provides authorization security in the application.
    /// </summary>
    public partial interface ISecurityAuthorization
    {

        /// <summary>
        /// Determine if the current context and access the security resource.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        bool AuthorizeSecurityResource(IContext context, ISecurityResource resource);

    }
}
