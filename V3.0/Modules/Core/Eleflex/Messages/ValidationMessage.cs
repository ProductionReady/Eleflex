using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a validation message displayed to the user.
    /// </summary>
    public partial class ValidationMessage : IValidationMessage
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ValidationMessage()
        {
            Fields = new List<string>();
        }

        /// <summary>
        /// Determine if an error.
        /// </summary>
        public virtual bool IsError { get; set; }

        /// <summary>
        /// The message displayed to the user.
        /// </summary>
        public virtual string UserMessage { get; set; }

        /// <summary>
        /// The message code.
        /// </summary>
        public virtual string MessageCode { get; set; }

        /// <summary>
        /// The field(s) this messages correlates to.
        /// </summary>
        public virtual IList<string> Fields { get; set; }
    }
}
