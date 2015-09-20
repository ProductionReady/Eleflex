using System;
using ServiceModel = Eleflex;

namespace Eleflex.Logging.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage service repository.
    /// </summary>
    public partial class LogMessageServiceRepository : ILogMessageServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.LogMessage> Insert(IRequestItem<ServiceModel.LogMessage> request)
        {
            using (ILoggingRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogMessageInsertRequest req = new LogMessageInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LogMessageInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.LogMessage> Get(IRequestItem<long> request)
        {
            using (ILoggingRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogMessageGetRequest req = new LogMessageGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LogMessageGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.LogMessage> Update(IRequestItem<ServiceModel.LogMessage> request)
        {
            using (ILoggingRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogMessageUpdateRequest req = new LogMessageUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LogMessageUpdateResponse>(req);
            }
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponse Delete(IRequestItem<long> request)
        {
            using (ILoggingRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogMessageDeleteRequest req = new LogMessageDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LogMessageDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceModel.LogMessage> Query(IRequestItem<IStorageQuery> request)
        {
            using (ILoggingRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogMessageQueryRequest req = new LogMessageQueryRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LogMessageQueryResponse>(req);
            }
        }

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
        {
            using (ILoggingRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogMessageQueryAggregateRequest req = new LogMessageQueryAggregateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LogMessageQueryAggregateResponse>(req);
            }
        }
    }
}
