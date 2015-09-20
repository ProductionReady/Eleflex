using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object that processes business rules for an object.
    /// </summary>
    public partial interface IBusinessRuleService
    {

        /// <summary>
        /// Execute all registered business rules for the given context object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IResponse ExecuteBusinessRules(IRequestItem<IContext> request);

        /// <summary>
        /// Execute a list of business rules with the given context.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        IResponse ExecuteBusinessRules(IRequestItem<IContext> request, IList<IBusinessRule> rules);

        /// <summary>
        /// Execute business rule events. This method throws exceptions, hence no use of request or response objects.
        /// </summary>
        /// <param name="context"></param>
        void ExecuteBusinessRuleEvents(IContext context);
    }
}
