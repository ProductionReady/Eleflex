using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object used to optionally define startup parameters for tasks.
    /// </summary>
    public partial interface ITaskOptions
    {

        /// <summary>
        /// Collection of diabled task names.
        /// </summary>
        IList<string> DisabledTaskNames { get; set; }

        /// <summary>
        /// Extra data.
        /// </summary>
        IDictionary<string, object> Data { get; set; }
    }
}
