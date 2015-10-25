using Eleflex.Storage.EF;
using DomainModel = Eleflex.Email;
using StorageModel = Eleflex.Email.Server;

namespace Eleflex.Email.Storage.EF
{
	/// <summary>
    /// Represents a EmailProcessAttachment storage repository.
    /// </summary>
	public partial class EmailProcessAttachmentStorageRepository : EntityMapStorageRepository<IEmailStorageService, DomainModel.EmailProcessAttachment, long, StorageModel.EmailProcessAttachment>, IEmailProcessAttachmentStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public EmailProcessAttachmentStorageRepository(
			IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IEmailStorageService storageService,
            IMappingService mappingService)
            : base(
					businessRuleService,
					contextBuilder,
					storageService,
					mappingService)
        { }  

	}
}
