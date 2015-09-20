using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityRole business repository.
    /// </summary>
    public partial class SecurityRoleBusinessRepository : BusinessRepository<SecurityRole, System.Guid>, ISecurityRoleBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public SecurityRoleBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<SecurityRole, System.Guid> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
