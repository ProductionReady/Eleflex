using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityUserClaim storage repository.
    /// </summary>
    public partial interface ISecurityUserClaimStorageRepository : IStorageRepository<SecurityUserClaim, long>
    {
    }
}

