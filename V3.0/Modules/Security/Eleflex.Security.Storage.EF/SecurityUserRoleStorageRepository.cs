using Eleflex.Storage.EF;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF
{
	/// <summary>
    /// Represents a SecurityUserRole storage repository.
    /// </summary>
	public partial class SecurityUserRoleStorageRepository : EntityMapStorageRepository<ISecurityStorageService, DomainModel.SecurityUserRole, long, StorageModel.SecurityUserRole>, ISecurityUserRoleStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public SecurityUserRoleStorageRepository(
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
