using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the SecurityUserClaim domain and a SecurityUserClaim storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class SecurityUserClaimAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserClaimAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the SecurityUserClaim domain and a SecurityUserClaim storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.SecurityUserClaim, StorageModel.SecurityUserClaim>();
            Mapper.CreateMap<StorageModel.SecurityUserClaim, DomainModel.SecurityUserClaim>();
            return base.Register(taskOptions);
        } 

	}
}
