using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eleflex
{
    /// <summary>
    /// Provides a single entry point for system startup task processing for the system.
    /// </summary>
    public static partial class SystemStartupShutdown
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
        public static bool Start(ITaskOptions taskOptions)
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

                //Get assemblies
                List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();
                Logger.Current.Debug(LOGGING_NAME, "[ASSEMBLIES LOADED] " + string.Join(" [,] ", assemblies.Select(x => x.FullName).ToList()));

                //Load all startup task types
                List<IStartupTask> startupList = new List<IStartupTask>();
                Type startupTaskType = typeof(IStartupTask);
                foreach (Assembly assembly in assemblies)
                {
                    List<Type> tasks = assembly.GetTypes().Where(x => startupTaskType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).ToList();
                    foreach (Type taskType in tasks)
                    {
                        //Create instance and add to list to be run
                        IStartupTask startTask = ObjectLocator.Current.GetInstance(taskType) as IStartupTask;
                        if(startTask != null)
                            startupList.Add(startTask);
                    }
                }

                //Order by priority
                startupList = startupList.OrderBy(x => x.Priority).ToList();
                Logger.Current.Debug(LOGGING_NAME, "[STARTUP TASKS LOADED] " + string.Join(" [,] ", startupList.Select(x=> x.Name).ToList()));

                //Start each task
                bool overallSuccess = true;
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
                            overallSuccess = false;
                        }
                    }
                    catch(Exception ex)
                    {
                        Logger.Current.Error(LOGGING_NAME, "[EXECUTING SYSTEM STARTUP TASK ERROR] " + task.Name, ex);
                        overallSuccess = false;
                    }
                }

                //Log and return overall success
                if (overallSuccess)
                    Logger.Current.Info(LOGGING_NAME, "[SYSTEM STARTUP SUCCESS]");
                else
                    Logger.Current.Error(LOGGING_NAME, "[SYSTEM STARTUP ERROR]");
                return overallSuccess;
            }
            catch(Exception ex)
            {
                Logger.Current.Fatal(LOGGING_NAME, "[SYSTEM STARTUP ERROR]", ex);
                return false;
            }
        }


        /// <summary>
        /// Start processesing startup tasks.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public static void Stop(ITaskOptions taskOptions)
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

                //Load all startup task types
                List<IShutdownTask> shutdownList = new List<IShutdownTask>();
                Type startupTaskType = typeof(IShutdownTask);
                foreach (Assembly assembly in assemblies)
                {
                    List<Type> tasks = assembly.GetTypes().Where(x => startupTaskType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).ToList();
                    foreach (Type taskType in tasks)
                    {
                        //Create instance and add to list to be run
                        IShutdownTask shutdownTask = ObjectLocator.Current.GetInstance(taskType) as IShutdownTask;
                        if (shutdownTask != null)
                            shutdownList.Add(shutdownTask);
                    }
                }

                //Order by priority
                shutdownList = shutdownList.OrderBy(x => x.Priority).ToList();
                Logger.Current.Debug(LOGGING_NAME, "[SHUTDOWN TASKS LOADED] " + string.Join(" [,] ", shutdownList.Select(x => x.Name).ToList()));

                //Start each task
                foreach(IShutdownTask task in shutdownList)
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
                        Logger.Current.Debug(LOGGING_NAME, "[EXECUTING SHUTDOWN TASK] " + task.Name + " [:] " + task.Description);
                        bool success = task.Stop(taskOptions);
                        if (success)
                            Logger.Current.Debug(LOGGING_NAME, "[EXECUTING SHUTDOWN TASK SUCCESS] " + task.Name);
                        else
                        {
                            Logger.Current.Error(LOGGING_NAME, "[EXECUTING SHUTDOWN TASK ERROR] " + task.Name);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Current.Error(LOGGING_NAME, "[EXECUTING SYSTEM SHUTDOWN TASK ERROR] " + task.Name, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Current.Fatal(LOGGING_NAME, "[SYSTEM SHUTDOWN ERROR]", ex);
            }
        }

    }
}
