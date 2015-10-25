using AutoMapper;
using DomainModel = Eleflex.Email;
using ServiceModel = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the EmailProcessAttachment domain and a EmailProcessAttachment service object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class EmailProcessAttachmentAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailProcessAttachmentAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the EmailProcessAttachment domain and a EmailProcessAttachment service object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.EmailProcessAttachment, ServiceModel.EmailProcessAttachment>();
            Mapper.CreateMap<ServiceModel.EmailProcessAttachment, DomainModel.EmailProcessAttachment>();
            return base.Register(taskOptions);
        } 

	}
}
