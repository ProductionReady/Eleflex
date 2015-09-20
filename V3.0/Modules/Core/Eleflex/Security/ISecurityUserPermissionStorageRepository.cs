using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityUserPermission storage repository.
    /// </summary>
    public partial interface ISecurityUserPermissionStorageRepository : IStorageRepository<SecurityUserPermission, long>
    {
    }
}

