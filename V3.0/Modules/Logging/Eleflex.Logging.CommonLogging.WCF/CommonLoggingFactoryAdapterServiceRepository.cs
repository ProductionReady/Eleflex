using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Eleflex.Logging.Services.WCF.Message;
using Eleflex.Services.WCF.OWIN;

namespace Eleflex.Logging.CommonLogging.WCF
{
    /// <summary>
    /// Common Logging Adapter used for creating log messages providing asynchronous capability. 
    /// This class instead utilizes a background thread for store messages using the service repository.
    /// </summary>
    public partial class CommonLoggingFactoryAdapterServiceRepository : CommonLoggingFactoryAdapter
    {


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="properties"></param>
        public CommonLoggingFactoryAdapterServiceRepository(NameValueCollection properties)
            :base(properties)
        {

        }


        /// <summary>
        /// Process the log message. (Store it somewhere)
        /// </summary>
        /// <param name="message"></param>
        public override bool ProcessMessage(Eleflex.LogMessage message)
        {
            //THIS METHOD IS EXECUTING ON A BACKGROUD THREAD!
            //We want to process logging on a different thread from the current request thread as to not slow down processing for persisting log messages.
            //Additionally messages created on the context of the request thread would be rolledback should the unit of work be rolledback/disposed.            
            try
            {
                //Get new instance each time as channel may be aborted
                ILogMessageServiceRepository repository = ObjectLocator.Current.GetInstance<ILogMessageServiceRepository>();
                using (var adminAccess = new ImpersonateSystem())
                    repository.Insert(new RequestItem<LogMessage>() { Item = message });
                return true;
            }
            catch (Exception ex)
            {
                Logger.Current.Error<CommonLoggingFactoryAdapterServiceRepository>(ex);
                return false;
            }            
        }

    }
}
