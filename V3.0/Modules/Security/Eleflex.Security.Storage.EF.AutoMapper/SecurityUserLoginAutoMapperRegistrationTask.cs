using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the SecurityUserLogin domain and a SecurityUserLogin storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class SecurityUserLoginAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserLoginAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the SecurityUserLogin domain and a SecurityUserLogin storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.SecurityUserLogin, StorageModel.SecurityUserLogin>();
            Mapper.CreateMap<StorageModel.SecurityUserLogin, DomainModel.SecurityUserLogin>();
            return base.Register(taskOptions);
        } 

	}
}
