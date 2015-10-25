using Eleflex;
using Eleflex.Services.WCF;
using Eleflex.Email.Services.WCF.Message;
using Eleflex.Security.ASPNetIdentity;

namespace WebClient.App_Start.Eleflex_Start.EleflexEmail
{
    /// <summary>
    /// Represents the ELEFLEX Email object location registration task configuring structuremap for the system.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class WebClientObjectLocationRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebClientObjectLocationRegistrationTask()
        {
            Description = "This tasks registers object location configurations for the ELEFLEX Email Module.";
        }

        /// <summary>
        /// Start registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            StructureMap.IContainer container = ObjectLocator.Container as StructureMap.IContainer;
            container.Configure(x =>
            {
                //Change ASP.NET OWIN email messaging service to use service repository
                x.For<IEmailIdentityMessageService>().Use<Eleflex.Email.WebClient.EmailIdentityMessageServiceRepositoryService>()
                    .Ctor<string>(Eleflex.Email.WebClient.EleflexEmailWebClientConstants.EMAILIDENTITYMESSAGESERVICE_CONSTRUCTORPARAM_FROMEMAILADDRESS).Is("support@eleflex.com"); //fromEmailAddress = "support@eleflex.com"

                //Email service request dispatcher
                x.For<IEmailRequestDispatcher>().Use<EmailRequestDispatcher>()
                    .Ctor<string>(WCFConstants.IWCFCOMMANDREQUESTDISPATCHER_CONSTRUCTORPARAM_ENDPOINT).Is(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT); //endpoint = "EleflexDefault" see web.config system.servicemodel.client

            });

            return base.Register(taskOptions);
        }
    }
}
