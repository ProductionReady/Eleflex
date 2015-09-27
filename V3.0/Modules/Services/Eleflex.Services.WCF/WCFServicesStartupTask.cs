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

            //Set command registry (needs to be reset for each run)
            WCFCommandRegistry.Current = new WCFCommandRegistryService();

            //Load all WCFCommands to the registry
            Type wcfCommandHandlerType = typeof(IWCFCommand);
            Type wcfCommandHandlerRegistrationType = typeof(WCFCommandRegistrationAttribute);

            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    //Find all IWCFCommand types
                    //It must be done this way because of system restarts, mismatched types due to multiple app domains being loaded
                    List<Type> commands = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Where(z => z.FullName == wcfCommandHandlerType.FullName).Any()).ToList();
                    foreach (Type command in commands)
                    {
                        //Use attribute to denote this command is exposed by the service
                        CustomAttributeData cad = command.CustomAttributes.Where(x => x.AttributeType.FullName == wcfCommandHandlerRegistrationType.FullName).FirstOrDefault();
                        if (cad != null)
                        {
                            WCFCommandRegistry.Current.RegisterItem(cad.ConstructorArguments[0].Value as Type, command);
                            WCFCommandRegistry.Current.RegisterItem(cad.ConstructorArguments[1].Value as Type, null);
                        }
                    }
                }//This may sometimes encounter ReflectionLoader errors for system references but these can be safely ignored
                catch { }
            }

            //Log the list of commands loaded
            Logger.Current.Debug<WCFServicesStartupTask>("[WCF COMMANDS LOADED] " + string.Join(" [,] ", WCFCommandRegistry.Current.RegistryCache.Keys.ToList().Select(x => x.FullName).ToList()));
            return base.Start(taskOptions);
        }

    }
}
