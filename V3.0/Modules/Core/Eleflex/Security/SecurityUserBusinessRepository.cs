using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityUser business repository.
    /// </summary>
    public partial class SecurityUserBusinessRepository : BusinessRepository<SecurityUser, System.Guid>, ISecurityUserBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public SecurityUserBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<SecurityUser, System.Guid> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
