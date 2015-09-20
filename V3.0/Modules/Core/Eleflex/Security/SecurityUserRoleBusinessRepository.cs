using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityUserRole business repository.
    /// </summary>
    public partial class SecurityUserRoleBusinessRepository : BusinessRepository<SecurityUserRole, long>, ISecurityUserRoleBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public SecurityUserRoleBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<SecurityUserRole, long> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
