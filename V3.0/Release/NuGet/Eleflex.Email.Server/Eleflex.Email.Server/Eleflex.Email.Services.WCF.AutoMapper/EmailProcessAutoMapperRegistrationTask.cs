using AutoMapper;
using DomainModel = Eleflex.Email;
using ServiceModel = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the EmailProcess domain and a EmailProcess service object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class EmailProcessAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailProcessAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the EmailProcess domain and a EmailProcess service object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.EmailProcess, ServiceModel.EmailProcess>();
            Mapper.CreateMap<ServiceModel.EmailProcess, DomainModel.EmailProcess>();
            return base.Register(taskOptions);
        } 

	}
}
