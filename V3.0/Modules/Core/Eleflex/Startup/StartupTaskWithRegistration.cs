using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eleflex
{
    /// <summary>
    /// Represents a StartupTask that additionally registers tasks.
    /// </summary>
    /// <typeparam name="TRegistrationAttribute"></typeparam>
    public abstract partial class StartupTaskWithRegistration<TRegistrationAttribute> : IStartupTask
        where TRegistrationAttribute : Attribute
    {

        private List<IRegistrationTask> _tasksToRun = new List<IRegistrationTask>();

        /// <summary>
        /// Constructor.
        /// </summary>
        public StartupTaskWithRegistration()
        {
            Name = this.GetType().FullName;            
            Priority = StartupConstants.PRIORITY_AFTERSYSTEM;
        }

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// THe description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// The execution priority.
        /// </summary>
        public virtual int Priority { get; set; }

        /// <summary>
        /// This signals the task to load resources it needs into the AppDomain.
        /// </summary>
        /// <returns></returns>
        public virtual void LoadResources()
        {
            _tasksToRun = new List<IRegistrationTask>();

            //Get RegistrationTasks to register and initialize
            Type registrationTaskType = typeof(IRegistrationTask);
            Type registrationAttributeType = typeof(TRegistrationAttribute);

            //Get assemblies
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();
            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    //Find all IRegistration types
                    //It must be done this way because of system restarts, mismatched types due to multiple app domains being loaded
                    List<Type> registrationTasks = assembly.GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Where(z => z.FullName == registrationTaskType.FullName).Any()).ToList();
                    foreach (Type taskType in registrationTasks)
                    {
                        CustomAttributeData cad = taskType.CustomAttributes.Where(x => x.AttributeType.FullName == registrationAttributeType.FullName).FirstOrDefault();
                        if (cad != null)
                        {
                            //Create instance and add to list to be run
                            IRegistrationTask regTask = ObjectLocator.Current.GetInstance(taskType) as IRegistrationTask;
                            if (regTask != null)
                                _tasksToRun.Add(regTask);
                        }
                    }
                }//This may sometimes encounter ReflectionLoader errors for system references but these can be safely ignored
                catch { }
            }
        }

        /// <summary>
        /// Perform startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public virtual bool Start(ITaskOptions taskOptions)
        {
            //Reload the resources
            LoadResources();

            //Get list of disabled task names
            List<string> disabledTaskNames = new List<string>();
            if (taskOptions != null && taskOptions.DisabledTaskNames != null)
                disabledTaskNames.AddRange(taskOptions.DisabledTaskNames);                              

            //Order tasks by priority
            List<IRegistrationTask> tasksToRun = _tasksToRun.OrderBy(x => x.Priority).ToList();
            Logger.Current.Debug<StartupTaskWithRegistration<TRegistrationAttribute>>("[REGISTRATION TASKS LOADED] " + string.Join(" [,] ", tasksToRun.Select(x => x.Name).ToList()));

            //Register each task
            bool overallSuccess = true;
            foreach (IRegistrationTask task in tasksToRun)
            {
                //Don't run disabled tasks
                if (disabledTaskNames.Contains(task.Name))
                {
                    Logger.Current.Debug<StartupTaskWithRegistration<TRegistrationAttribute>>("[SKIPPING DISABLED TASK] " + task.Name + " [:]" + task.Description);
                    continue;
                }

                try
                {
                    //Execute the task
                    Logger.Current.Debug<StartupTaskWithRegistration<TRegistrationAttribute>>("[EXECUTING REGISTRATION TASK] " + task.Name + " [:] " + task.Description);
                    bool success = task.Register(taskOptions);
                    if (success)
                        Logger.Current.Debug<StartupTaskWithRegistration<TRegistrationAttribute>>("[EXECUTING REGISTRATION TASK SUCCESS] " + task.Name);
                    else
                    {
                        Logger.Current.Debug<StartupTaskWithRegistration<TRegistrationAttribute>>("[EXECUTING REGISTRATION TASK ERROR] " + task.Name);
                        overallSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Current.Error<StartupTaskWithRegistration<TRegistrationAttribute>>("[EXECUTING REGISTRATION TASK ERROR] " + task.Name, ex);
                    overallSuccess = false;
                }
            }

            //Return overall success
            return overallSuccess;
        }

    }
}
