using System;
using System.Collections.Generic;
using System.Linq;

namespace Eleflex
{
    /// <summary>
    /// Represents an object that processes business rules for an object.
    /// </summary>
    public partial class BusinessRuleService : IBusinessRuleService
    {        

        /// <summary>
        /// Execute business rule events. This method throws exceptions, hence no use of request or response objects.
        /// </summary>
        /// <param name="context"></param>
        public virtual void ExecuteBusinessRuleEvents(IContext context)
        {
            //Execute events same as other business rules
            IResponse response = ExecuteBusinessRules(new RequestItem<IContext>() { Item = context });

            //If an error happened, throw as managed Eleflex exception
            if(!response.ResponseSuccess)
            {
                EleflexException ex = new EleflexException();
                foreach (var item in response.ResponseMessages)
                    ex.ResponseMessages.Add(item);
                throw ex;
            }
        }

        /// <summary>
        /// Execute all registered business rules for the given context object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponse ExecuteBusinessRules(IRequestItem<IContext> request)
        {            
            Response response = new Response();
            try
            {
                //input validation
                if (request == null || request.Item == null || request.Item.Item == null)
                    return response; //Invalid request, bad coding, but allow with no error

                //Find all business rule types for this object type
                Type itemType = request.Item.Item.GetType();
                IList<Type> registry = BusinessRuleRegistry.Current.GetRegistryList(itemType);
                if (registry == null || registry.Count == 0)
                    return response; //No rules found, ok
                
                //Create each type and add to list to execute
                List<IBusinessRule> rules = new List<IBusinessRule>();
                foreach (Type type in registry)
                {
                    IBusinessRule rule = ObjectLocator.Current.GetInstance(type) as IBusinessRule;
                    if (rule != null)
                        rules.Add(rule);
                }

                //No rules created
                if (rules.Count == 0)
                    return response;

                //Execute the actual rules found
                return ExecuteBusinessRules(request, rules);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<BusinessRuleService>(ex);
                response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                return response;
            }
        }

        /// <summary>
        /// Execute a list of business rules with the given context.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        public virtual IResponse ExecuteBusinessRules(IRequestItem<IContext> request, IList<IBusinessRule> rules)
        {
            Response response = new Response();
            try
            {
                //Disable business rule options from context
                IList<string> disabledBusinessRuleTypes = new List<string>();
                if (request.Item.Data.ContainsKey(BusinessRuleConstants.OPTION_DISABLE_BUSINESS_RULE_TYPES))
                    disabledBusinessRuleTypes = request.Item.Data[BusinessRuleConstants.OPTION_DISABLE_BUSINESS_RULE_TYPES] as IList<string>;

                //Disable business rule options from exclusion
                foreach (IBusinessRule rule in rules)
                {
                    if (rule is IBusinessRuleExclusion)
                        disabledBusinessRuleTypes.Add(((IBusinessRuleExclusion)rule).ExcludedBusinessRuleName);
                }

                //Order rules by priority
                var orderedRules = rules.OrderBy(x => x.Priority);

                //Process each rule (or ruleset)
                foreach (IBusinessRule rule in orderedRules)
                {
                    //Skip disabled business rules by context and exlusion
                    if (disabledBusinessRuleTypes.Contains(rule.GetType().FullName))
                        continue;

                    //Execute rule
                    IResponse resp = rule.Execute(request);
                    response.CopyResponse(resp);
                    if (!resp.ResponseSuccess && rule.StopOnError)
                        break;
                }                

                //Return response
                if (response.ResponseSuccess)
                    return response;
                else if (response.ResponseMessages.Where(x => x.IsError == true).Any())
                    return response;
                else
                {
                    //Make sure has at least one error message if an overall error
                    response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                    return response;
                }
            }
            catch (Exception ex)
            {
                Logger.Current.Error<BusinessRuleService>(ex);
                response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                return response;
            }
        }

    }
}
