using AutoMapper;
using DomainModel = Eleflex.Email;
using StorageModel = Eleflex.Email.Server;

namespace Eleflex.Email.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the EmailProcess domain and a EmailProcess storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class EmailProcessAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailProcessAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the EmailProcess domain and a EmailProcess storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.EmailProcess, StorageModel.EmailProcess>();
            Mapper.CreateMap<StorageModel.EmailProcess, DomainModel.EmailProcess>();
            return base.Register(taskOptions);
        } 

	}
}
