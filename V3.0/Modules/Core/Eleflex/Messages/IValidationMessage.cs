using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a validation message displayed to the user.
    /// </summary>
    public partial interface IValidationMessage
    {
        /// <summary>
        /// Determine if an error.
        /// </summary>
        bool IsError { get; set; }

        /// <summary>
        /// The message displayed to the user.
        /// </summary>
        string UserMessage { get; set; }

        /// <summary>
        /// The message code.
        /// </summary>
        string MessageCode { get; set; }

        /// <summary>
        /// The field(s) this messages correlates to.
        /// </summary>
        IList<string> Fields { get; set; }
    }
}
