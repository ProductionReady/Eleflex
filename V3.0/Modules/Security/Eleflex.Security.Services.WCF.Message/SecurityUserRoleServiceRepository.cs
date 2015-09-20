using System;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service client.
    /// </summary>
    public partial class SecurityUserRoleServiceRepository : ISecurityUserRoleServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityUserRole> Insert(IRequestItem<ServiceModel.SecurityUserRole> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserRoleInsertRequest req = new SecurityUserRoleInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserRoleInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityUserRole> Get(IRequestItem<long> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserRoleGetRequest req = new SecurityUserRoleGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserRoleGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityUserRole> Update(IRequestItem<ServiceModel.SecurityUserRole> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserRoleUpdateRequest req = new SecurityUserRoleUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserRoleUpdateResponse>(req);
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
                SecurityUserRoleDeleteRequest req = new SecurityUserRoleDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserRoleDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceModel.SecurityUserRole> Query(IRequestItem<IStorageQuery> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserRoleQueryRequest req = new SecurityUserRoleQueryRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserRoleQueryResponse>(req);
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
                SecurityUserRoleQueryAggregateRequest req = new SecurityUserRoleQueryAggregateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserRoleQueryAggregateResponse>(req);
            }
        }
    }
}
