using Eleflex.Storage.EF;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Versioning.Storage.EF;

namespace Eleflex.Versioning.Storage.EF
{
	/// <summary>
    /// Represents a Module storage repository.
    /// </summary>
	public partial class ModuleStorageRepository : EntityMapStorageRepository<IVersioningStorageService, DomainModel.Module, System.Guid, StorageModel.Module>, IModuleStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public ModuleStorageRepository(
			IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            IVersioningStorageService storageService,
            IMappingService mappingService)
            : base(
					businessRuleService,
					contextBuilder,
					storageService,
					mappingService)
        { }  

	}
}
