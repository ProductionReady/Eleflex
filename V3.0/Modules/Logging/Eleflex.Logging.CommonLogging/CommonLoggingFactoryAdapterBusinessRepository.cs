using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Common.Logging;

namespace Eleflex.Logging.CommonLogging
{
    /// <summary>
    /// Common Logging Adapter used for creating log messages providing asynchronous capability. 
    /// This class instead utilizes a background thread for store messages using the business repository.
    /// </summary>
    public partial class CommonLoggingFactoryAdapterBusinessRepository : CommonLoggingFactoryAdapter
    {


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="properties"></param>
        public CommonLoggingFactoryAdapterBusinessRepository(NameValueCollection properties)
            :base(properties)
        {

        }


        private static ILogMessageBusinessRepository _repository = null;
        private static IStorageContextUnitOfWork _uow = null;

        /// <summary>
        /// Process the log message. (Store it somewhere)
        /// </summary>
        /// <param name="message"></param>
        public override bool ProcessMessage(Eleflex.LogMessage message)
        {
            //THIS METHOD IS EXECUTING ON A BACKGROUD THREAD!
            //We want to process logging on a different thread from the current request thread as to not slow down processing for persisting log messages and also because
            //messages created on the context of the request thread would be rolledback should the unit of work be rolledback/disposed.

            //Using object locator will create a new instance for each business repository in the uow created, filling up uow list as processes ages, so only get one repository instance.
            if(_uow == null)
                _uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
            try
            {                
                ILogMessageBusinessRepository logRepository = _repository;
                if (logRepository == null)
                {
                    _repository = ObjectLocator.Current.GetInstance<ILogMessageBusinessRepository>();
                    logRepository = _repository;
                }
                var resp = logRepository.Insert(new RequestItem<Eleflex.LogMessage>() { Item = message });
                _uow.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _uow.Rollback();
                Logger.Current.Error<CommonLoggingFactoryAdapterBusinessRepository>(ex);
                return false;
            }
            //Don't call dispose on the unit of work as this is a long running thread. Calling Commit or Rollback will reset work regardless.
        }

    }
}
