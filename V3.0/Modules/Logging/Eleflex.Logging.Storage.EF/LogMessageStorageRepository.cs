using Eleflex.Storage.EF;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Logging.Storage.EF;

namespace Eleflex.Logging.Storage.EF
{
	/// <summary>
    /// Represents a LogMessage storage repository.
    /// </summary>
	public partial class LogMessageStorageRepository : EntityMapStorageRepository<ILoggingStorageService, DomainModel.LogMessage, long, StorageModel.LogMessage>, ILogMessageStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public LogMessageStorageRepository(
			IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            ILoggingStorageService storageService,
            IMappingService mappingService)
            : base(
					businessRuleService,
					contextBuilder,
					storageService,
					mappingService)
        { }  

	}
}
