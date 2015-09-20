using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityUser storage repository.
    /// </summary>
    public partial interface ISecurityUserStorageRepository : IStorageRepository<SecurityUser, System.Guid>
    {
    }
}

