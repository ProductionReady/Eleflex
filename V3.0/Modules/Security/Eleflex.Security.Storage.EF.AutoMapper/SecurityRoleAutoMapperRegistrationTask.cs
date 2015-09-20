using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the SecurityRole domain and a SecurityRole storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class SecurityRoleAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityRoleAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the SecurityRole domain and a SecurityRole storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.SecurityRole, StorageModel.SecurityRole>();
            Mapper.CreateMap<StorageModel.SecurityRole, DomainModel.SecurityRole>();
            return base.Register(taskOptions);
        } 

	}
}
