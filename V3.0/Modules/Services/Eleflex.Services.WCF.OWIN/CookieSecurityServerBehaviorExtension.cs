using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Behavior extension to add the cookie message inspector.
    /// </summary>
    public partial class CookieSecurityServerBehaviorExtension : BehaviorExtensionElement, IServiceBehavior
    {

        /// <summary>
        /// The behavior type.
        /// </summary>
        public override Type BehaviorType
        {
            get { return typeof(CookieSecurityServerBehaviorExtension); }
        }

        /// <summary>
        /// Create behavior.
        /// </summary>
        /// <returns></returns>
        protected override object CreateBehavior()
        {
            return new CookieSecurityServerBehaviorExtension();
        }

        /// <summary>
        /// Add binding parameters.
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        /// <param name="endpoints"></param>
        /// <param name="bindingParameters"></param>
        public virtual void AddBindingParameters(ServiceDescription serviceDescription,
               ServiceHostBase serviceHostBase,
               System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
               BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Apply dispatch behavior.
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (var endpoint in dispatcher.Endpoints)
                {
                    //Add message inspector
                    endpoint.DispatchRuntime.MessageInspectors.Add(new CookieSecurityServerMessageInspector());
                }
            }
        }

        /// <summary>
        /// Validate.
        /// </summary>
        /// <param name="serviceDescription"></param>
        /// <param name="serviceHostBase"></param>
        public virtual void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
