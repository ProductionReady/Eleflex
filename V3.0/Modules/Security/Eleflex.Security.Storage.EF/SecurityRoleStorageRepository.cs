using Eleflex.Storage.EF;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF
{
	/// <summary>
    /// Represents a SecurityRole storage repository.
    /// </summary>
	public partial class SecurityRoleStorageRepository : EntityMapStorageRepository<ISecurityStorageService, DomainModel.SecurityRole, System.Guid, StorageModel.SecurityRole>, ISecurityRoleStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public SecurityRoleStorageRepository(
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
