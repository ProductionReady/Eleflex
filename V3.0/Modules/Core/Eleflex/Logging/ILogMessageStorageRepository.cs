using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a LogMessage storage repository.
    /// </summary>
    public partial interface ILogMessageStorageRepository : IStorageRepository<LogMessage, long>
    {
    }
}

