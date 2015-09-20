using Eleflex;
using Eleflex.Services.WCF;
using Eleflex.Lookups.Services.WCF.Message;

namespace $rootnamespace$.App_Start.Eleflex_Start
{
    /// <summary>
    /// Represents the ELEFLEX Lookups object location registration task configuring structuremap for the system.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class EleflexLookupsWebClientObjectLocationRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexLookupsWebClientObjectLocationRegistrationTask()
        {
            Description = "This tasks registers object location configurations for the ELEFLEX Lookups Module.";
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
                //Lookups service request dispatcher
                x.For<ILookupsRequestDispatcher>().Use<LookupsRequestDispatcher>()
                    .Ctor<string>(WCFConstants.IWCFCOMMANDREQUESTDISPATCHER_CONSTRUCTORPARAM_ENDPOINT).Is(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT); //endpoint = "EleflexDefault" see web.config system.servicemodel.client

            });

            return base.Register(taskOptions);
        }
    }
}
