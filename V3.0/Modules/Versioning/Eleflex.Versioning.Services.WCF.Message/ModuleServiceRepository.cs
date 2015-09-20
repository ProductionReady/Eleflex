using System;
using ServiceModel = Eleflex;

namespace Eleflex.Versioning.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service repository.
    /// </summary>
    public partial class ModuleServiceRepository : IModuleServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.Module> Insert(IRequestItem<ServiceModel.Module> request)
        {
            using (IVersioningRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleInsertRequest req = new ModuleInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<ModuleInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.Module> Get(IRequestItem<System.Guid> request)
        {
            using (IVersioningRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleGetRequest req = new ModuleGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<ModuleGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.Module> Update(IRequestItem<ServiceModel.Module> request)
        {
            using (IVersioningRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleUpdateRequest req = new ModuleUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<ModuleUpdateResponse>(req);
            }
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponse Delete(IRequestItem<System.Guid> request)
        {
            using (IVersioningRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleDeleteRequest req = new ModuleDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<ModuleDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceModel.Module> Query(IRequestItem<IStorageQuery> request)
        {
            using (IVersioningRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleQueryRequest req = new ModuleQueryRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<ModuleQueryResponse>(req);
            }
        }

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
        {
            using (IVersioningRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleQueryAggregateRequest req = new ModuleQueryAggregateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<ModuleQueryAggregateResponse>(req);
            }
        }
    }
}
