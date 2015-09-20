using Eleflex.Storage.EF;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF
{
	/// <summary>
    /// Represents a SecurityRolePermission storage repository.
    /// </summary>
	public partial class SecurityRolePermissionStorageRepository : EntityMapStorageRepository<ISecurityStorageService, DomainModel.SecurityRolePermission, long, StorageModel.SecurityRolePermission>, ISecurityRolePermissionStorageRepository
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
		/// <param name="businessRuleService"></param>
		/// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        public SecurityRolePermissionStorageRepository(
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
