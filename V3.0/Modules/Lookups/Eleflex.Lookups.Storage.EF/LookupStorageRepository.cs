using Eleflex.Storage.EF;
using DomainModel = Eleflex.Lookups;
using StorageModel = Eleflex.Lookups.Storage.EF;

namespace Eleflex.Lookups.Storage.EF
{
	/// <summary>
    /// Represents a Lookup storage repository.
    /// </summary>
	public partial class LookupStorageRepository : EntityMapStorageRepository<ILookupsStorageService, DomainModel.Lookup, System.Guid, StorageModel.Lookup>, ILookupStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public LookupStorageRepository(
			IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            ILookupsStorageService storageService,
            IMappingService mappingService)
            : base(
					businessRuleService,
					contextBuilder,
					storageService,
					mappingService)
        { }  

	}
}
