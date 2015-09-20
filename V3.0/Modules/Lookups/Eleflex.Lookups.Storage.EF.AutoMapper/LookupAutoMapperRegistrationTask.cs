using AutoMapper;
using DomainModel = Eleflex.Lookups;
using StorageModel = Eleflex.Lookups.Storage.EF;

namespace Eleflex.Lookups.Storage.EF.AutoMapper
{
	/// <summary>
    /// Represents a mapping registration task for mapping between the Lookup domain and a Lookup storage object.
    /// </summary>
	[MappingRegistrationTask()]
	public partial class LookupAutoMapperRegistrationTask : RegistrationTask
	{	
	 
        /// <summary>
        /// Constructor.
        /// </summary>
        public LookupAutoMapperRegistrationTask()
        {
            Description = "This task registers mapping between the Lookup domain and a Lookup storage object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
		/// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            Mapper.CreateMap<DomainModel.Lookup, StorageModel.Lookup>();
            Mapper.CreateMap<StorageModel.Lookup, DomainModel.Lookup>();
            return base.Register(taskOptions);
        } 

	}
}
