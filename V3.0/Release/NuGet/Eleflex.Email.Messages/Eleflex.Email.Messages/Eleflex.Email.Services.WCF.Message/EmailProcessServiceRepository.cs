using System;
using ServiceMessages = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service client.
    /// </summary>
    public partial class EmailProcessServiceRepository : IEmailProcessServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceMessages.EmailProcess> Insert(IRequestItem<ServiceMessages.EmailProcess> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessInsertRequest req = new EmailProcessInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<EmailProcessInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceMessages.EmailProcess> Get(IRequestItem<long> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessGetRequest req = new EmailProcessGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<EmailProcessGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceMessages.EmailProcess> Update(IRequestItem<ServiceMessages.EmailProcess> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessUpdateRequest req = new EmailProcessUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<EmailProcessUpdateResponse>(req);
            }
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponse Delete(IRequestItem<long> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessDeleteRequest req = new EmailProcessDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<EmailProcessDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceMessages.EmailProcess> Query(IRequestItem<IStorageQuery> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessQueryRequest req = new EmailProcessQueryRequest();
                if (request.Item != null)
                {
                    req.Item.StorageQueryFilters = request.Item.StorageQueryFilters;
                    req.Item.PagingNumberPerPage = request.Item.PagingNumberPerPage;
                    req.Item.PagingStartPage = request.Item.PagingStartPage;
                }
                return dispatcher.ExecuteServiceCommand<EmailProcessQueryResponse>(req);
            }
        }

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessQueryAggregateRequest req = new EmailProcessQueryAggregateRequest();
                if (request.Item != null)
                {
                    req.Item.StorageQueryFilters = request.Item.StorageQueryFilters;
                    req.Item.PagingNumberPerPage = request.Item.PagingNumberPerPage;
                    req.Item.PagingStartPage = request.Item.PagingStartPage;
                }
                return dispatcher.ExecuteServiceCommand<EmailProcessQueryAggregateResponse>(req);
            }
        }
    }
}
