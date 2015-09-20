using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityUserLogin storage repository.
    /// </summary>
    public partial interface ISecurityUserLoginStorageRepository : IStorageRepository<SecurityUserLogin, long>
    {
    }
}

