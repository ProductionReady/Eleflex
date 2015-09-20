namespace Eleflex
{
    /// <summary>
    /// Represents a security resource that can be authorized against.
    /// </summary>
    public partial interface ISecurityResource
    {

        /// <summary>
        /// The resource key.
        /// </summary>
        string SecurityResourceKey { get; set; }

        /// <summary>
        /// The resource type.
        /// </summary>
        string SecurityResourceType { get; set; }

        /// <summary>
        /// The access requested to the resource.
        /// </summary>
        string SecurityAccessRequested { get; set; }
        
    }
}
