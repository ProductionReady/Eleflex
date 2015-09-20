using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object used to optionally define startup parameters for tasks.
    /// </summary>
    public partial class TaskOptions : ITaskOptions
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public TaskOptions()
        {
            DisabledTaskNames = new List<string>();
            Data = new Dictionary<string, object>();
        }

        /// <summary>
        /// Collection of diabled task names.
        /// </summary>
        public virtual IList<string> DisabledTaskNames { get; set; }

        /// <summary>
        /// Extra data.
        /// </summary>
        public virtual IDictionary<string, object> Data { get; set; }

    }
}
