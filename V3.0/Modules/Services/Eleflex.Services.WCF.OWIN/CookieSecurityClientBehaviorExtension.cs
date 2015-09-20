using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents an object that provides client cookie security behavior.
    /// </summary>
    public partial class CookieSecurityClientBehaviorExtension : BehaviorExtensionElement, IEndpointBehavior
    {

        /// <summary>
        /// Behavior type.
        /// </summary>
        public override System.Type BehaviorType
        {
            get { return typeof(CookieSecurityClientBehaviorExtension); }
        }

        /// <summary>
        /// Create behavior.
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            return new CookieSecurityClientBehaviorExtension();
        }

        /// <summary>
        /// Add binding parameters
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="bindingParameters"></param>
        public virtual void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Appy client behavior.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientRuntime"></param>
        public virtual void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            //Add message inspector
            clientRuntime.MessageInspectors.Add(new CookieSecurityClientMessageInspector());
        }

        /// <summary>
        /// Apply dispatch behavior.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="endpointDispatcher"></param>
        public virtual void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {

        }

        /// <summary>
        /// Validate.
        /// </summary>
        /// <param name="endpoint"></param>
        public virtual void Validate(ServiceEndpoint endpoint)
        {

        }

    }
}
