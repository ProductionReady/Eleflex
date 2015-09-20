using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the SecurityUserRole domain and a SecurityUserRole storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class SecurityUserRoleAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserRoleAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the SecurityUserRole domain and a SecurityUserRole storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.SecurityUserRole, StorageModel.SecurityUserRole>();
            Mapper.CreateMap<StorageModel.SecurityUserRole, DomainModel.SecurityUserRole>();
            return base.Register(taskOptions);
        } 

	}
}
