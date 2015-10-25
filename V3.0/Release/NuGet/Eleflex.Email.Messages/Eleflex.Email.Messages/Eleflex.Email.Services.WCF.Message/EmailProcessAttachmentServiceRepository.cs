using System;
using ServiceMessages = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service client.
    /// </summary>
    public partial class EmailProcessAttachmentServiceRepository : IEmailProcessAttachmentServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceMessages.EmailProcessAttachment> Insert(IRequestItem<ServiceMessages.EmailProcessAttachment> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessAttachmentInsertRequest req = new EmailProcessAttachmentInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<EmailProcessAttachmentInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceMessages.EmailProcessAttachment> Get(IRequestItem<long> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessAttachmentGetRequest req = new EmailProcessAttachmentGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<EmailProcessAttachmentGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceMessages.EmailProcessAttachment> Update(IRequestItem<ServiceMessages.EmailProcessAttachment> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessAttachmentUpdateRequest req = new EmailProcessAttachmentUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<EmailProcessAttachmentUpdateResponse>(req);
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
                EmailProcessAttachmentDeleteRequest req = new EmailProcessAttachmentDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<EmailProcessAttachmentDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceMessages.EmailProcessAttachment> Query(IRequestItem<IStorageQuery> request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                EmailProcessAttachmentQueryRequest req = new EmailProcessAttachmentQueryRequest();
                if (request.Item != null)
                {
                    req.Item.StorageQueryFilters = request.Item.StorageQueryFilters;
                    req.Item.PagingNumberPerPage = request.Item.PagingNumberPerPage;
                    req.Item.PagingStartPage = request.Item.PagingStartPage;
                }
                return dispatcher.ExecuteServiceCommand<EmailProcessAttachmentQueryResponse>(req);
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
                EmailProcessAttachmentQueryAggregateRequest req = new EmailProcessAttachmentQueryAggregateRequest();
                if (request.Item != null)
                {
                    req.Item.StorageQueryFilters = request.Item.StorageQueryFilters;
                    req.Item.PagingNumberPerPage = request.Item.PagingNumberPerPage;
                    req.Item.PagingStartPage = request.Item.PagingStartPage;
                }
                return dispatcher.ExecuteServiceCommand<EmailProcessAttachmentQueryAggregateResponse>(req);
            }
        }
    }
}
