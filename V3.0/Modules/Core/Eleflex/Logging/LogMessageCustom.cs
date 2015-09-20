using System;

namespace Eleflex
{
    /// <summary>
    /// Represents an object for a LogMessage.
    /// </summary>
    public partial class LogMessage
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public LogMessage()
        {
            CreateDate = DateTimeOffset.UtcNow;
        }
    }
}
