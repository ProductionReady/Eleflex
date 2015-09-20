using System.Threading;
using System.Collections.Specialized;
using Eleflex;
using Eleflex.Logging.CommonLogging;

namespace $rootnamespace$.App_Start.Eleflex_Start
{
    /// <summary>
    /// Represents the logging startup task.
    /// </summary>
    public partial class SystemLoggingStartupTask : StartupTask
    {

        /// <summary>
        /// Consstructor.
        /// </summary>
        public SystemLoggingStartupTask() : base()
        {
            Description = @"This task registers the logging provider used by the system.";
            Priority = StartupConstants.STARTUP_PRIORITY_LOGGING;
        }

        /// <summary>
        /// Start processing logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {
            //Create properties to configure CommonLogging logging
            NameValueCollection properties = new NameValueCollection();
            properties.Add(LoggingConstants.CONFIGPARAM_APPLICATION, "ELEFLEX Web Server Application");
            properties.Add(LoggingConstants.CONFIGPARAM_SERVER, System.Net.Dns.GetHostName());
            properties.Add(LoggingConstants.CONFIGPARAM_FATAL, true.ToString());
            properties.Add(LoggingConstants.CONFIGPARAM_ERROR, true.ToString());
            properties.Add(LoggingConstants.CONFIGPARAM_INFO, true.ToString());
            properties.Add(LoggingConstants.CONFIGPARAM_WARN, true.ToString());
            properties.Add(LoggingConstants.CONFIGPARAM_DEBUG, false.ToString());
            properties.Add(LoggingConstants.CONFIGPARAM_TRACE, false.ToString());

            //Setup common logging adapter for using LogMessage business repository. Use CommonLoggingFactoryAdapterServiceRepository for client apps.
            Common.Logging.LogManager.Adapter = new CommonLoggingFactoryAdapterBusinessRepository(properties);

            //Swap existing log provider
            ILogService existingProvider = Interlocked.Exchange<ILogService>(ref Logger.Current, new CommonLoggingService());

            //If existing provider is MemoryLog, write out its contents to the new provider for storage
            if (existingProvider != null && existingProvider is MemoryLogService)
            {
                MemoryLogService memoryLog = existingProvider as MemoryLogService;
                foreach (var log in memoryLog.List)
                    Logger.Current.Log(log);
            }            

            return base.Start(taskOptions);
        }
    }
}
