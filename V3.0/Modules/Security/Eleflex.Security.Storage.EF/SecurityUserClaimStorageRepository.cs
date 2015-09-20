using Eleflex.Storage.EF;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF
{
	/// <summary>
    /// Represents a SecurityUserClaim storage repository.
    /// </summary>
	public partial class SecurityUserClaimStorageRepository : EntityMapStorageRepository<ISecurityStorageService, DomainModel.SecurityUserClaim, long, StorageModel.SecurityUserClaim>, ISecurityUserClaimStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public SecurityUserClaimStorageRepository(
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
