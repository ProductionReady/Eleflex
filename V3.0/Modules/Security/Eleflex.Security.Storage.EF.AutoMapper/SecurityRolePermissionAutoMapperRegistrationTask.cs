using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the SecurityRolePermission domain and a SecurityRolePermission storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class SecurityRolePermissionAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityRolePermissionAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the SecurityRolePermission domain and a SecurityRolePermission storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.SecurityRolePermission, StorageModel.SecurityRolePermission>();
            Mapper.CreateMap<StorageModel.SecurityRolePermission, DomainModel.SecurityRolePermission>();
            return base.Register(taskOptions);
        } 

	}
}
