using AutoMapper;
using DomainModel = Eleflex;
using SecurityModel = Eleflex.Security.ASPNetIdentity;

namespace Eleflex.Security.ASPNetIdentity.AutoMapper
{
    /// <summary>
    /// Represents a mapping registration task for mapping object between the security domain and identity model.
    /// </summary>
    [MappingRegistrationTask()]
    public partial class AutoMapperRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public AutoMapperRegistrationTask()
        {
            Description = "This task registers all mapping between the identity model with the security domain and service models.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {

            Mapper.CreateMap<DomainModel.SecurityUser, SecurityModel.IdentityUser>();
            Mapper.CreateMap<SecurityModel.IdentityUser, DomainModel.SecurityUser>();

            Mapper.CreateMap<DomainModel.SecurityRole, SecurityModel.IdentityRole>();
            Mapper.CreateMap<SecurityModel.IdentityRole, DomainModel.SecurityRole>();

            return base.Register(taskOptions);
        }
    }
}
