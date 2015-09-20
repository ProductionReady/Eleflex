using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityRole storage repository.
    /// </summary>
    public partial interface ISecurityRoleStorageRepository : IStorageRepository<SecurityRole, System.Guid>
    {
    }
}

