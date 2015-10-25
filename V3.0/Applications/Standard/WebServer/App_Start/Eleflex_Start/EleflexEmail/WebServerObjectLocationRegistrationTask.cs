using Eleflex;
using Eleflex.Email;
using Eleflex.Email.Storage.EF;

namespace WebServer.App_Start.Eleflex_Start.EleflexEmail
{
    /// <summary>
    /// Represents the ELEFLEX Email object location registration task configuring structuremap for the system.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class WebServerObjectLocationRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebServerObjectLocationRegistrationTask()
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
                //Email storage service
                x.For<IEmailStorageService>().Use<EmailStorageService>()
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_CONNECTIONSTRING).Is(StorageConstants.CONNECTIONSTRING_KEY_DEFAULT) //connectionString = "EleflexDefault" see web.config connectionStrings
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_VERSIONINGSTORAGECONFIG).Is(Eleflex.Email.Storage.EF.SQLServer.EmailSQLServerConstants.VERSIONING_STORAGE_CONFIG);
            });

            return base.Register(taskOptions);
        }
    }
}
