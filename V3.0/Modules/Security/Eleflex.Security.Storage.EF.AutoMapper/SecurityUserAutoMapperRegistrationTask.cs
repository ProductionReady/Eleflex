using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the SecurityUser domain and a SecurityUser storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class SecurityUserAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the SecurityUser domain and a SecurityUser storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.SecurityUser, StorageModel.SecurityUser>();
            Mapper.CreateMap<StorageModel.SecurityUser, DomainModel.SecurityUser>();
            return base.Register(taskOptions);
        } 

	}
}
