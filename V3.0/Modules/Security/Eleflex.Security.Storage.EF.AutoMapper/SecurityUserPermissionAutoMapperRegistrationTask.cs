using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the SecurityUserPermission domain and a SecurityUserPermission storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class SecurityUserPermissionAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserPermissionAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the SecurityUserPermission domain and a SecurityUserPermission storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.SecurityUserPermission, StorageModel.SecurityUserPermission>();
            Mapper.CreateMap<StorageModel.SecurityUserPermission, DomainModel.SecurityUserPermission>();
            return base.Register(taskOptions);
        } 

	}
}
