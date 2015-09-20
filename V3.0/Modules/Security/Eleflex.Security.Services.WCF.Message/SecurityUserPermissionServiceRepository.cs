using System;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service client.
    /// </summary>
    public partial class SecurityUserPermissionServiceRepository : ISecurityUserPermissionServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityUserPermission> Insert(IRequestItem<ServiceModel.SecurityUserPermission> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserPermissionInsertRequest req = new SecurityUserPermissionInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserPermissionInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityUserPermission> Get(IRequestItem<long> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserPermissionGetRequest req = new SecurityUserPermissionGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserPermissionGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityUserPermission> Update(IRequestItem<ServiceModel.SecurityUserPermission> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserPermissionUpdateRequest req = new SecurityUserPermissionUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserPermissionUpdateResponse>(req);
            }
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponse Delete(IRequestItem<long> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserPermissionDeleteRequest req = new SecurityUserPermissionDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserPermissionDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceModel.SecurityUserPermission> Query(IRequestItem<IStorageQuery> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserPermissionQueryRequest req = new SecurityUserPermissionQueryRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserPermissionQueryResponse>(req);
            }
        }

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserPermissionQueryAggregateRequest req = new SecurityUserPermissionQueryAggregateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserPermissionQueryAggregateResponse>(req);
            }
        }
    }
}
