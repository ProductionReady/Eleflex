using System;

namespace Eleflex.Email
{
    /// <summary>
    /// Represents a EmailProcessAttachment business repository.
    /// </summary>
    public partial class EmailProcessAttachmentBusinessRepository : BusinessRepository<EmailProcessAttachment, long>, IEmailProcessAttachmentBusinessRepository
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageRepository"></param>
        public EmailProcessAttachmentBusinessRepository(
            IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IStorageRepository<EmailProcessAttachment, long> storageRepository)
            : base(
                  businessRuleService,
				  contextBuilder,
                  storageRepository)
        {
        }
    }
}
