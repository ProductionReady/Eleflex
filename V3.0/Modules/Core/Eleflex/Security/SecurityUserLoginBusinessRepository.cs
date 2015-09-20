using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityUserLogin business repository.
    /// </summary>
    public partial class SecurityUserLoginBusinessRepository : BusinessRepository<SecurityUserLogin, long>, ISecurityUserLoginBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public SecurityUserLoginBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<SecurityUserLogin, long> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
