using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object containing contextual information.
    /// </summary>
    public partial interface IContext : IItem<object>
    {

        /// <summary>
        /// Additional data that can is sent along with the request.
        /// </summary>
        IDictionary<string, object> Data { get; set; }

        /// <summary>
        /// The authenticated user stored in the context.
        /// </summary>
        ISecurityUser User { get; set; }
        
    }
}
