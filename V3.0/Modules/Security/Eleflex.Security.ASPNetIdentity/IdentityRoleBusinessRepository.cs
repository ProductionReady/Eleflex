using System;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an object used for an IdentityRole business repository.
    /// </summary>
    public partial class IdentityRoleBusinessRepository : MappingRepository<IdentityRole, Guid, SecurityRole, ISecurityRoleBusinessRepository>, IIdentityRoleBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapping"></param>
        public IdentityRoleBusinessRepository(
            ISecurityRoleBusinessRepository repository,
            IMappingService mapping)
            : base(repository, mapping)
        {
        }
    }
}
