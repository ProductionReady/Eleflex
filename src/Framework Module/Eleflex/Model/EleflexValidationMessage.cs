using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex
{
    /// <summary>
    /// Defines a validation message.
    /// </summary>
    public class EleflexValidationMessage
    {

        /// <summary>
        /// Determine if an error.
        /// </summary>
        public virtual bool IsError { get; set; }

        /// <summary>
        /// The message displayed to the user.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// The message code (if any, used for localization or other processing rules).
        /// </summary>
        public virtual string MessageCode { get; set; }

        /// <summary>
        /// The field this messages correlates to.
        /// </summary>
        public virtual string Field { get; set; }
    }
}
