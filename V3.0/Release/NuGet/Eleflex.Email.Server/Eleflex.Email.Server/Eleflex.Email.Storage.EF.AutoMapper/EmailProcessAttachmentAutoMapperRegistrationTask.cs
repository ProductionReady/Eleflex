using AutoMapper;
using DomainModel = Eleflex.Email;
using StorageModel = Eleflex.Email.Server;

namespace Eleflex.Email.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the EmailProcessAttachment domain and a EmailProcessAttachment storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class EmailProcessAttachmentAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailProcessAttachmentAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the EmailProcessAttachment domain and a EmailProcessAttachment storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.EmailProcessAttachment, StorageModel.EmailProcessAttachment>();
            Mapper.CreateMap<StorageModel.EmailProcessAttachment, DomainModel.EmailProcessAttachment>();
            return base.Register(taskOptions);
        } 

	}
}
