using System;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an IdentityUser business repository.
    /// </summary>
    public partial interface IIdentityUserBusinessRepository : IBusinessRepository<IdentityUser, Guid>
    {
    }
}
