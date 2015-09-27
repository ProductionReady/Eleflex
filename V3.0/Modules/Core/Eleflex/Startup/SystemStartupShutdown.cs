using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eleflex
{
    /// <summary>
    /// Provides a single entry point for system startup task processing for the system.
    /// </summary>
    public partial class SystemStartupShutdown : ISystemStartupShutdown
    {

        /// <summary>
        /// The name used for logging purposes.
        /// </summary>
        public const string LOGGING_NAME = "Eleflex.SystemStartupShutdown";


        /// <summary>
        /// Start processesing startup tasks.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public virtual void Start(ITaskOptions taskOptions)
        {
            //Get list of disabled task names
            List<string> disabledTaskNames = new List<string>();
            if (taskOptions != null && taskOptions.DisabledTaskNames != null)
                disabledTaskNames.AddRange(taskOptions.DisabledTaskNames);
            try
            {
                //Setup default object location
                if (ObjectLocator.Current == null)
                    ObjectLocator.Current = new ActivatorObjectLocationService();

                //Setup default logging
                if (Logger.Current == null)
                    Logger.Current = new MemoryLogService();

                //Debug list of disabled startups                
                Logger.Current.Debug(LOGGING_NAME, "[SYSTEM STARTUP BEGIN]");
                Logger.Current.Debug(LOGGING_NAME, "[DISABLED TASK NAMES LOADED] " + string.Join(" [,] ", disabledTaskNames));

                //Start first process. Because there are multiple app domains loaded, this should cause all other assemblies to be loaded in AppDomain
                Logger.Current.Debug(LOGGING_NAME, "[STARTING FIRST PROCESS TO LOAD ALL ASSEMBLIES IN APPDOMAIN]");
                StartupProcedure(taskOptions, disabledTaskNames);
                Logger.Current.Debug(LOGGING_NAME, "[STARTING SECOND PROCESS WITH ALL ASSEMBLIES LOADED]");
                //Start second process. We should have all assemblies loaded in the AppDomain now, re-run all
                StartupProcedure(taskOptions, disabledTaskNames);

                //Log 
                Logger.Current.Info(LOGGING_NAME, "[SYSTEM STARTUP SUCCESS]");
            }
            catch(Exception ex)
            {
                Logger.Current.Fatal(LOGGING_NAME, "[SYSTEM STARTUP ERROR]", ex);
            }
        }

        protected virtual void StartupProcedure(ITaskOptions taskOptions, List<string> disabledTaskNames)
        {
            //Get assemblies
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();

            Logger.Current.Debug(LOGGING_NAME, "[ASSEMBLIES LOADED] " + string.Join(" [,] ", assemblies.Select(x => x.FullName).ToList()));

            //Load all startup task types
            List<IStartupTask> startupList = new List<IStartupTask>();
            Type startupTaskType = typeof(IStartupTask);
            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    //Find all IStartupTask types
                    //It must be done this way because of system restarts, mismatched types due to multiple app domains being loaded
                    List<Type> tasks = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Where(z => z.FullName == startupTaskType.FullName).Any()).ToList();
                    foreach (Type taskType in tasks)
                    {
                        //Create instance and add to list to be run
                        IStartupTask startTask = ObjectLocator.Current.GetInstance(taskType) as IStartupTask;
                        if (startTask != null)
                            startupList.Add(startTask);
                    }
                }//This may sometimes encounter ReflectionLoader errors for system references but these can be safely ignored
                catch { }
            }

            //Order by priority
            startupList = startupList.OrderBy(x => x.Priority).ToList();
            Logger.Current.Debug(LOGGING_NAME, "[STARTUP TASKS LOADED] " + string.Join(" [,] ", startupList.Select(x => x.Name).ToList()));

            //Start each task
            foreach (IStartupTask task in startupList)
            {
                //Don't run disabled tasks
                if (disabledTaskNames.Contains(task.Name))
                {
                    Logger.Current.Debug(LOGGING_NAME, "[SKIPPING DISABLED TASK] " + task.Name + " [:]" + task.Description);
                    continue;
                }

                try
                {
                    //Execute the task
                    Logger.Current.Debug(LOGGING_NAME, "[EXECUTING STARTUP TASK] " + task.Name + " [:] " + task.Description);
                    bool success = task.Start(taskOptions);
                    if (success)
                        Logger.Current.Debug(LOGGING_NAME, "[EXECUTING STARTUP TASK SUCCESS] " + task.Name);
                    else
                    {
                        Logger.Current.Error(LOGGING_NAME, "[EXECUTING STARTUP TASK ERROR] " + task.Name);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Current.Error(LOGGING_NAME, "[EXECUTING SYSTEM STARTUP TASK ERROR] " + task.Name, ex);
                }
            }
        }


        /// <summary>
        /// Start processesing startup tasks.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public virtual void Stop(ITaskOptions taskOptions)
        {
            //Get list of disabled task names
            List<string> disabledTaskNames = new List<string>();
            if (taskOptions != null && taskOptions.DisabledTaskNames != null)
                disabledTaskNames.AddRange(taskOptions.DisabledTaskNames);
            try
            {
                Logger.Current.Info(LOGGING_NAME, "[SYSTEM SHUTDOWN]");

                //Debug list of disabled startups                
                Logger.Current.Debug(LOGGING_NAME, "[DISABLED TASK NAMES LOADED] " + string.Join(" [,] ", disabledTaskNames));

                //Get assemblies
                List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();

                Logger.Current.Debug(LOGGING_NAME, "[ASSEMBLIES LOADED] " + string.Join(" [,] ", assemblies.Select(x => x.FullName).ToList()));

                //Load all shutdown task types
                List<IShutdownTask> shutdownList = new List<IShutdownTask>();
                Type shutdownTaskType = typeof(IShutdownTask);
                foreach (Assembly assembly in assemblies)
                {
                    try
                    {
                        //Find all IShutdownTask types
                        //It must be done this way because of system restarts, mismatched types due to multiple app domains being loaded
                        List<Type> tasks = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Where(z => z.FullName == shutdownTaskType.FullName).Any()).ToList();
                        foreach (Type taskType in tasks)
                        {
                            //Create instance and add to list to be run
                            IShutdownTask shutdownTask = ObjectLocator.Current.GetInstance(taskType) as IShutdownTask;
                            if (shutdownTask != null)
                                shutdownList.Add(shutdownTask);
                        }
                    }//This may sometimes encounter ReflectionLoader errors for system references but these can be safely ignored
                    catch { }
                }

                //Order by priority
                shutdownList = shutdownList.OrderBy(x => x.Priority).ToList();
                Logger.Current.Debug(LOGGING_NAME, "[SHUTDOWN TASKS LOADED] " + string.Join(" [,] ", shutdownList.Select(x => x.Name).ToList()));

                //DON'T LOG ANY MORE MESSAGES AS LOGGING MAY BE SHUTDOWN

                //Stop each task
                foreach (IShutdownTask task in shutdownList)
                {
                    //Don't run disabled tasks
                    if (disabledTaskNames.Contains(task.Name))
                    {
                        continue;
                    }

                    try
                    {
                        //Execute the task
                        bool success = task.Stop(taskOptions);
                    }
                    catch { }
                }
            }
            catch { }
        }

    }
}
