using System;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represent an IdentityUser business repository.
    /// </summary>
    public partial class IdentityUserBusinessRepository : MappingRepository<IdentityUser, Guid, Eleflex.SecurityUser, ISecurityUserBusinessRepository>, IIdentityUserBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapping"></param>
        public IdentityUserBusinessRepository(
            ISecurityUserBusinessRepository repository,
            IMappingService mapping)
            : base(repository, mapping)
        {
        }
        
    }
}
