using Eleflex;
using Eleflex.Lookups;
using Eleflex.Lookups.Storage.EF;

namespace $rootnamespace$.App_Start.Eleflex_Start.EleflexLookups
{
    /// <summary>
    /// Represents the ELEFLEX Lookups object location registration task configuring structuremap for the system.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class WebServerObjectLocationRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebServerObjectLocationRegistrationTask()
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
                //Lookups storage service
                x.For<ILookupsStorageService>().Use<LookupsStorageService>()
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_CONNECTIONSTRING).Is(StorageConstants.CONNECTIONSTRING_KEY_DEFAULT) //connectionString = "EleflexDefault" see web.config connectionStrings
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_VERSIONINGSTORAGECONFIG).Is(Eleflex.Lookups.Storage.EF.SQLServer.LookupsSQLServerConstants.VERSIONING_STORAGE_CONFIG);
            });

            return base.Register(taskOptions);
        }
    }
}
