using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a SecurityPermission business repository.
    /// </summary>
    public partial class SecurityPermissionBusinessRepository : BusinessRepository<SecurityPermission, System.Guid>, ISecurityPermissionBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public SecurityPermissionBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<SecurityPermission, System.Guid> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
