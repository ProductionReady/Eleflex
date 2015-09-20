using AutoMapper;
using ServiceModel = Eleflex.Security.Services.WCF.Message;
using WebModel = Eleflex.Security.Web.Account.Models;
using Eleflex.Security.ASPNetIdentity;

namespace Eleflex.Security.Web.Account
{
    [MappingRegistrationTask]
    public partial class AutoMapperRegistrationTask : RegistrationTask
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AutoMapperRegistrationTask()
        {
            Description = "This task registers all mapping between the web model with the service and identity models.";
        }

        public override bool Register(ITaskOptions taskOptions)
        {
            return base.Register(taskOptions);
        }
    }
}
