using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityUserClaim business repository.
    /// </summary>
    public partial class SecurityUserClaimBusinessRepository : BusinessRepository<SecurityUserClaim, long>, ISecurityUserClaimBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public SecurityUserClaimBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<SecurityUserClaim, long> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
