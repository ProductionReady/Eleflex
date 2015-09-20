using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityRolePermission business repository.
    /// </summary>
    public partial class SecurityRolePermissionBusinessRepository : BusinessRepository<SecurityRolePermission, long>, ISecurityRolePermissionBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public SecurityRolePermissionBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<SecurityRolePermission, long> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
