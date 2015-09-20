using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Security.Storage.EF;

namespace Eleflex.Security.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the SecurityPermission domain and a SecurityPermission storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class SecurityPermissionAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityPermissionAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the SecurityPermission domain and a SecurityPermission storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.SecurityPermission, StorageModel.SecurityPermission>();
            Mapper.CreateMap<StorageModel.SecurityPermission, DomainModel.SecurityPermission>();
            return base.Register(taskOptions);
        } 

	}
}
