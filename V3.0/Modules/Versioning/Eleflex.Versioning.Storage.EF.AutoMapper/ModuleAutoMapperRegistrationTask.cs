using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Versioning.Storage.EF;

namespace Eleflex.Versioning.Storage.EF.SQLServer.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the Module domain and a Module storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class ModuleAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public ModuleAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the Module domain and a Module storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.Module, StorageModel.Module>();
            Mapper.CreateMap<StorageModel.Module, DomainModel.Module>();
            return base.Register(taskOptions);
        } 

	}
}
