using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eleflex
{
    /// <summary>
    /// Startup task for business rules that additionally processes registration tasks.
    /// </summary>
    public partial class BusinessRuleStartupTask : StartupTaskWithRegistration<BusinessRuleRegistrationTaskAttribute>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public BusinessRuleStartupTask()
        {
            Description = @"This task registers all business rules for the system.";
            Priority = StartupConstants.STARTUP_PRIORITY_BUSINESSRULES;
        }

        /// <summary>
        /// Start the task logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {
            //Get assemblies
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();

            //Find business rules and add them all to the registry
            Type businessRuleType = typeof(IBusinessRule);
            Type businessRuleProcessType = typeof(BusinessRuleProcessAttribute);
            List<Type> loadedRules = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                //Find all IBusinessRule types
                List<Type> rules = assembly.GetTypes().Where(x => businessRuleType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).ToList();                
                foreach (Type ruleType in rules)
                {
                    //Use attribute to denote the model object this rule applies to
                    CustomAttributeData cad = ruleType.CustomAttributes.Where(x => x.AttributeType == businessRuleProcessType).FirstOrDefault();
                    if (cad != null)
                    {
                        loadedRules.Add(ruleType);
                        BusinessRuleRegistry.Current.RegisterItem(cad.ConstructorArguments[0].Value as Type, ruleType);
                    }
                }
            }

            //Log the list of rules loaded            
            Logger.Current.Debug<BusinessRuleStartupTask>("[BUSINESS RULES LOADED] " + string.Join(" [,] ", loadedRules.Select(x => x.FullName).ToList()));
            return base.Start(taskOptions);
        }
    }
}
