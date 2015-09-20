using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityUserPermission business repository.
    /// </summary>
    public partial class SecurityUserPermissionBusinessRepository : BusinessRepository<SecurityUserPermission, long>, ISecurityUserPermissionBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public SecurityUserPermissionBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<SecurityUserPermission, long> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
