using AutoMapper;
using DomainModel = Eleflex.Lookups;
using ServiceModel = Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the Lookup domain and a Lookup service object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class LookupAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public LookupAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the Lookup domain and a Lookup service object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.Lookup, ServiceModel.Lookup>();
            Mapper.CreateMap<ServiceModel.Lookup, DomainModel.Lookup>();
            return base.Register(taskOptions);
        } 

	}
}
