using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object containing contextual information.
    /// </summary>
    public partial class Context : IContext
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public Context()
        {
            Data = new Dictionary<string, object>();
        }

        /// <summary>
        /// Additional data that can is sent along with the request.
        /// </summary>
        public virtual IDictionary<string, object> Data { get; set; }

        /// <summary>
        /// The authenticated user stored in the context.
        /// </summary>
        public virtual ISecurityUser User { get; set; }

        /// <summary>
        /// The object the business rule is enforcing logic against.
        /// </summary>
        public virtual object Item { get; set; }

    }
}
