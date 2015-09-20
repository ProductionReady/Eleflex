using AutoMapper;
using DomainModel = Eleflex;
using StorageModel = Eleflex.Logging.Storage.EF;

namespace Eleflex.Logging.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the LogMessage domain and a LogMessage storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class LogMessageAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public LogMessageAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the LogMessage domain and a LogMessage storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.LogMessage, StorageModel.LogMessage>();
            Mapper.CreateMap<StorageModel.LogMessage, DomainModel.LogMessage>();
            return base.Register(taskOptions);
        } 

	}
}
