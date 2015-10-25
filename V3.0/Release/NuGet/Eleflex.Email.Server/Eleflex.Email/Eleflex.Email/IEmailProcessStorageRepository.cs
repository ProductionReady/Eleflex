using System;
using System.Collections.Generic;

namespace Eleflex.Email
{
    /// <summary>
    /// Represents a EmailProcess storage repository.
    /// </summary>
    public partial interface IEmailProcessStorageRepository : IStorageRepository<EmailProcess, long>
    {
    }
}

