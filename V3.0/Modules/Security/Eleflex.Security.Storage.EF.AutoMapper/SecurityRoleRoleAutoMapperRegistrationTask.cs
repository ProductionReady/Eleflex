using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the SecurityRoleRole domain and a SecurityRoleRole storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class SecurityRoleRoleAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityRoleRoleAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the SecurityRoleRole domain and a SecurityRoleRole storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.SecurityRoleRole, StorageModel.SecurityRoleRole>();
            Mapper.CreateMap<StorageModel.SecurityRoleRole, DomainModel.SecurityRoleRole>();
            return base.Register(taskOptions);
        } 

	}
}
