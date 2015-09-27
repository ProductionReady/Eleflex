using Eleflex;
using Eleflex.Services.WCF;
using Eleflex.Security.ASPNetIdentity;
using Eleflex.Services.WCF.OWIN;
using Microsoft.AspNet.Identity;
using Eleflex.Logging.Storage.EF;
using Eleflex.Logging.Services.WCF.Message;
using Eleflex.Security.Storage.EF;
using Eleflex.Security.Services.WCF.Message;
using Eleflex.Versioning.Storage.EF;
using Eleflex.Versioning.Services.WCF.Message;

namespace $rootnamespace$.App_Start.Eleflex_Start
{
    /// <summary>
    /// Represents the object location registration task for configuring structuremap to use specified objects for the system.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class SystemObjectLocationRegistrationTask : RegistrationTask
    {
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public SystemObjectLocationRegistrationTask()
        {
            Description = "This tasks registers core eleflex object location configurations for the system.";
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
                //CONFIGURE LOGGING
                //Logging storage service
                x.For<ILoggingStorageService>().Use<LoggingStorageService>()
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_CONNECTIONSTRING).Is(StorageConstants.CONNECTIONSTRING_KEY_DEFAULT) //connectionString = "EleflexDefault" see web.config connectionStrings
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_VERSIONINGSTORAGECONFIG).Is(Eleflex.Logging.Storage.EF.SQLServer.LoggingSQLServerConstants.VERSIONING_STORAGE_CONFIG);

                //Logging service request dispatcher
                x.For<Eleflex.Logging.Services.WCF.Message.ILoggingRequestDispatcher>().Use<LoggingRequestDispatcher>()
                    .Ctor<string>(WCFConstants.IWCFCOMMANDREQUESTDISPATCHER_CONSTRUCTORPARAM_ENDPOINT).Is(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT); //endpoint = "EleflexDefault" see web.config system.servicemodel.client

                //CONFIGURE VERSIONING
                //Versioning storage service
                x.For<IVersioningStorageService>().Use<VersioningStorageService>()
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_CONNECTIONSTRING).Is(StorageConstants.CONNECTIONSTRING_KEY_DEFAULT) //connectionString = "EleflexDefault" see web.config connectionStrings
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_VERSIONINGSTORAGECONFIG).Is(Eleflex.Versioning.Storage.EF.SQLServer.VersioningSQLServerConstants.VERSIONING_STORAGE_CONFIG);

                //Versioning service request dispatcher
                x.For<IVersioningRequestDispatcher>().Use<VersioningRequestDispatcher>()
                    .Ctor<string>(WCFConstants.IWCFCOMMANDREQUESTDISPATCHER_CONSTRUCTORPARAM_ENDPOINT).Is(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT); //endpoint = "EleflexDefault" see web.config system.servicemodel.client

                //CONFIGURE SECURITY
                //Security storage service
                x.For<ISecurityStorageService>().Use<SecurityStorageService>()
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_CONNECTIONSTRING).Is(StorageConstants.CONNECTIONSTRING_KEY_DEFAULT) //connectionString = "EleflexDefault" see web.config connectionStrings
                    .Ctor<string>(StorageConstants.ISTORAGESERVICE_CONSTUCTORPARAM_VERSIONINGSTORAGECONFIG).Is(Eleflex.Security.Storage.EF.SQLServer.SecuritySQLServerConstants.VERSIONING_STORAGE_CONFIG);

                //Security service request dispatcher
                x.For<ISecurityRequestDispatcher>().Use<SecurityRequestDispatcher>()
                    .Ctor<string>(WCFConstants.IWCFCOMMANDREQUESTDISPATCHER_CONSTRUCTORPARAM_ENDPOINT).Is(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT); //endpoint = "EleflexDefault" see web.config system.servicemodel.client
                //Security ASP.NET Identity. For client apps, use IdentityUserStoreServiceRepository and IdentityRoleStoreServiceRepository                
                x.For<IUserStore<IdentityUser>>().Use<IdentityUserStoreBusinessRepository>();
                x.For<IRoleStore<IdentityRole>>().Use<IdentityRoleStoreBusinessRepository>();
            });

            return base.Register(taskOptions);
        }
    }
}
