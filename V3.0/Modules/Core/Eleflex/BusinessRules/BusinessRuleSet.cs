using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a list of business rule objects.
    /// </summary>
    public partial class BusinessRuleSet : BusinessRule, IBusinessRuleSet
    {

        /// <summary>
        /// The business rule service to execute rules against.
        /// </summary>
        protected readonly IBusinessRuleService _businessRuleService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
        public BusinessRuleSet(IBusinessRuleService businessRuleService)
        {
            _businessRuleService = businessRuleService;
            Items = new List<IBusinessRule>();
        }

        /// <summary>
        /// The collection of business rules in the set.
        /// </summary>
        public virtual IList<IBusinessRule> Items { get; set; }

        /// <summary>
        /// Execute the business rule logic.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override IResponse Execute(IRequestItem<IContext> request)
        {
            return _businessRuleService.ExecuteBusinessRules(request, Items);            
        }
    }
}
