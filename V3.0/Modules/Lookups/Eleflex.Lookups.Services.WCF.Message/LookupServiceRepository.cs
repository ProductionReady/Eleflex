using System;
using ServiceMessages = Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service client.
    /// </summary>
    public partial class LookupServiceRepository : ILookupServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceMessages.Lookup> Insert(IRequestItem<ServiceMessages.Lookup> request)
        {
            using (ILookupsRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupInsertRequest req = new LookupInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LookupInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceMessages.Lookup> Get(IRequestItem<System.Guid> request)
        {
            using (ILookupsRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupGetRequest req = new LookupGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LookupGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceMessages.Lookup> Update(IRequestItem<ServiceMessages.Lookup> request)
        {
            using (ILookupsRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupUpdateRequest req = new LookupUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LookupUpdateResponse>(req);
            }
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponse Delete(IRequestItem<System.Guid> request)
        {
            using (ILookupsRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupDeleteRequest req = new LookupDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<LookupDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceMessages.Lookup> Query(IRequestItem<IStorageQuery> request)
        {
            using (ILookupsRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupQueryRequest req = new LookupQueryRequest();
                if (request.Item != null)
                {
                    req.Item.StorageQueryFilters = request.Item.StorageQueryFilters;
                    req.Item.PagingNumberPerPage = request.Item.PagingNumberPerPage;
                    req.Item.PagingStartPage = request.Item.PagingStartPage;
                }
                return dispatcher.ExecuteServiceCommand<LookupQueryResponse>(req);
            }
        }

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
        {
            using (ILookupsRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupQueryAggregateRequest req = new LookupQueryAggregateRequest();
                if (request.Item != null)
                {
                    req.Item.StorageQueryFilters = request.Item.StorageQueryFilters;
                    req.Item.PagingNumberPerPage = request.Item.PagingNumberPerPage;
                    req.Item.PagingStartPage = request.Item.PagingStartPage;
                }
                return dispatcher.ExecuteServiceCommand<LookupQueryAggregateResponse>(req);
            }
        }
    }
}
