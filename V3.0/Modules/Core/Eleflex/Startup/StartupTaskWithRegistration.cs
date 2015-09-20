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
        /// Perform startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public virtual bool Start(ITaskOptions taskOptions)
        {
            //Get list of disabled task names
            List<string> disabledTaskNames = new List<string>();
            if (taskOptions != null && taskOptions.DisabledTaskNames != null)
                disabledTaskNames.AddRange(taskOptions.DisabledTaskNames);

            //Get assemblies
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();

            //Get RegistrationTasks to register and initialize
            Type registrationTaskType = typeof(IRegistrationTask);
            Type registrationAttributeType = typeof(TRegistrationAttribute);
            List<IRegistrationTask> tasksToRun = new List<IRegistrationTask>();
            
            foreach (Assembly assembly in assemblies)
            {
                //Find all IRegistration types
                List<Type> registrationTasks = assembly.GetTypes().Where(x => registrationTaskType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).ToList();
                foreach (Type taskType in registrationTasks)
                {                    
                    if(TypeShouldBeRegistered(taskType, registrationAttributeType))                    
                    {
                        //Create instance and add to list to be run
                        IRegistrationTask regTask = ObjectLocator.Current.GetInstance(taskType) as IRegistrationTask;
                        if (regTask != null)
                            tasksToRun.Add(regTask);
                    }
                }
            }

            //Order tasks by priority
            tasksToRun = tasksToRun.OrderBy(x => x.Priority).ToList();
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

        /// <summary>
        /// Determine if a type should be registered.
        /// </summary>
        /// <param name="objectType"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        protected virtual bool TypeShouldBeRegistered(Type objectType, Type attributeType)
        {
            //Use attribute to denote that object should be loaded
            CustomAttributeData cad = objectType.CustomAttributes.Where(x => x.AttributeType == attributeType).FirstOrDefault();
            return cad != null;
        }

    }
}
