using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents a startup task for the system used for WCF services.
    /// </summary>
    public partial class WCFServicesStartupTask : StartupTaskWithRegistration<WCFServicesRegistrationTaskAttribute>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WCFServicesStartupTask()
        {
            Description = @"This task registers all WCF communication configurations needed by the system.";
            Priority = StartupConstants.STARTUP_PRIORITY_SERVICECOMMUNICATION;
        }

        /// <summary>
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {            
            //Get assemblies
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();

            //Load all WCFCommands to the registry
            Type wcfCommandHandlerType = typeof(IWCFCommand);
            Type wcfCommandHandlerRegistrationType = typeof(WCFCommandRegistrationAttribute);                        

            foreach (Assembly assembly in assemblies)
            {
                //Find all IWCFCommand types
                List<Type> commands = assembly.GetTypes().Where(x => wcfCommandHandlerType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).ToList();
                foreach (Type command in commands)
                {
                    //Use attribute to denote this command is exposed by the service
                    CustomAttributeData cad = command.CustomAttributes.Where(x => x.AttributeType == wcfCommandHandlerRegistrationType).FirstOrDefault();
                    if (cad != null)
                    {
                        WCFCommandRegistry.Current.RegisterItem(cad.ConstructorArguments[0].Value as Type, command);
                        WCFCommandRegistry.Current.RegisterItem(cad.ConstructorArguments[1].Value as Type, null);
                    }
                }
            }

            //Log the list of commands loaded
            Logger.Current.Debug<WCFServicesStartupTask>("[WCF COMMANDS LOADED] " + string.Join(" [,] ", WCFCommandRegistry.Current.RegistryCache.Keys.ToList().Select(x => x.FullName).ToList()));
            return base.Start(taskOptions);
        }
    }
}
