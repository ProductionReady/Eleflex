using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityRoleRole business repository.
    /// </summary>
    public partial class SecurityRoleRoleBusinessRepository : BusinessRepository<SecurityRoleRole, long>, ISecurityRoleRoleBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public SecurityRoleRoleBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<SecurityRoleRole, long> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
