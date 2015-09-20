using Eleflex.Storage.EF;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF
{
	/// <summary>
    /// Represents a SecurityUserLogin storage repository.
    /// </summary>
	public partial class SecurityUserLoginStorageRepository : EntityMapStorageRepository<ISecurityStorageService, DomainModel.SecurityUserLogin, long, StorageModel.SecurityUserLogin>, ISecurityUserLoginStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public SecurityUserLoginStorageRepository(
			IBusinessRuleService businessRuleService,
			IContextBuilder contextBuilder,
            ISecurityStorageService storageService,
            IMappingService mappingService)
            : base(
					businessRuleService,
					contextBuilder,
					storageService,
					mappingService)
        { }  

	}
}
