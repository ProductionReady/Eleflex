namespace Eleflex
{
    /// <summary>
    /// Represents a security resource that can be authorized against.
    /// </summary>
    public partial class SecurityResource : ISecurityResource
    {
        /// <summary>
        /// The resource key.
        /// </summary>
        public virtual string SecurityResourceKey { get; set; }

        /// <summary>
        /// The resource type.
        /// </summary>
        public virtual string SecurityResourceType { get; set; }


        /// <summary>
        /// The access requested to the resource.
        /// </summary>
        public virtual string SecurityAccessRequested { get; set; }
    }
}
