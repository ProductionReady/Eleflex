namespace Eleflex
{
    /// <summary>
    /// Extension method added to all BusinessRuleEvent objects to raise/fire the event in the system.
    /// </summary>
    public static partial class BusinessRuleEventExtensions
    {

        /// <summary>
        /// Raise/fire the event for application integration.
        /// </summary>
        /// <param name="businessRuleEvent">The object that is going to be raised.</param>
        public static void RaiseEvent(this IBusinessRuleEvent businessRuleEvent)
        {
            //Get context builder and assign the object
            var contextBuilder = ObjectLocator.Current.GetInstance<IContextBuilder>();
            IContext context = contextBuilder.GetContext();
            context.Item = businessRuleEvent;

            //Get the business rule service and process the event
            IBusinessRuleService businessRuleService = ObjectLocator.Current.GetInstance<IBusinessRuleService>();
            businessRuleService.ExecuteBusinessRuleEvents(context);
        }
    }
}
