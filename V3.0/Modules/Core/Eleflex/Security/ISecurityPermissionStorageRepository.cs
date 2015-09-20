using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityPermission storage repository.
    /// </summary>
    public partial interface ISecurityPermissionStorageRepository : IStorageRepository<SecurityPermission, System.Guid>
    {
    }
}

