using Eleflex.Storage.EF;
using DomainModel = Eleflex.Email;
using StorageModel = Eleflex.Email.Server;

namespace Eleflex.Email.Storage.EF
{
	/// <summary>
    /// Represents a EmailProcess storage repository.
    /// </summary>
	public partial class EmailProcessStorageRepository : EntityMapStorageRepository<IEmailStorageService, DomainModel.EmailProcess, long, StorageModel.EmailProcess>, IEmailProcessStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public EmailProcessStorageRepository(
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
