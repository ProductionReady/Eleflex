using System;

namespace Eleflex.Email
{
    /// <summary>
    /// Represents a EmailProcess business repository.
    /// </summary>
    public partial class EmailProcessBusinessRepository : BusinessRepository<EmailProcess, long>, IEmailProcessBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public EmailProcessBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<EmailProcess, long> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
