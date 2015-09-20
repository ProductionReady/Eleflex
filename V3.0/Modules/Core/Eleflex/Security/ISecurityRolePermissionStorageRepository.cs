using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityRolePermission storage repository.
    /// </summary>
    public partial interface ISecurityRolePermissionStorageRepository : IStorageRepository<SecurityRolePermission, long>
    {
    }
}

