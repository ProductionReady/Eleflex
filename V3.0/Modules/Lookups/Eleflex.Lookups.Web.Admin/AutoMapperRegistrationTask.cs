using AutoMapper;
using ServiceModel = Eleflex.Lookups.Services.WCF.Message;
using WebModel = Eleflex.Lookups.Web.Admin.Models;
using Eleflex.Security.ASPNetIdentity;

namespace Eleflex.Lookups.Web.Admin
{
    [MappingRegistrationTask]
    public partial class AutoMapperRegistrationTask : RegistrationTask
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AutoMapperRegistrationTask()
        {
            Description = "This task registers all mapping between the web model with the service model.";
        }

        public override bool Register(ITaskOptions taskOptions)
        {


            //Users
            Mapper.CreateMap<WebModel.EditViewModel, ServiceModel.Lookup>();
            Mapper.CreateMap<ServiceModel.Lookup, WebModel.EditViewModel>();            

            return base.Register(taskOptions);
        }
    }
}
