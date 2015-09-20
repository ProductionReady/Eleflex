using System;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service client.
    /// </summary>
    public partial class SecurityRolePermissionServiceRepository : ISecurityRolePermissionServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityRolePermission> Insert(IRequestItem<ServiceModel.SecurityRolePermission> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityRolePermissionInsertRequest req = new SecurityRolePermissionInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRolePermissionInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityRolePermission> Get(IRequestItem<long> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityRolePermissionGetRequest req = new SecurityRolePermissionGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRolePermissionGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityRolePermission> Update(IRequestItem<ServiceModel.SecurityRolePermission> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityRolePermissionUpdateRequest req = new SecurityRolePermissionUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRolePermissionUpdateResponse>(req);
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
                SecurityRolePermissionDeleteRequest req = new SecurityRolePermissionDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRolePermissionDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceModel.SecurityRolePermission> Query(IRequestItem<IStorageQuery> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityRolePermissionQueryRequest req = new SecurityRolePermissionQueryRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRolePermissionQueryResponse>(req);
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
                SecurityRolePermissionQueryAggregateRequest req = new SecurityRolePermissionQueryAggregateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRolePermissionQueryAggregateResponse>(req);
            }
        }
    }
}
