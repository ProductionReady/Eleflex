using System;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service client.
    /// </summary>
    public partial class SecurityRoleRoleServiceRepository : ISecurityRoleRoleServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityRoleRole> Insert(IRequestItem<ServiceModel.SecurityRoleRole> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityRoleRoleInsertRequest req = new SecurityRoleRoleInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRoleRoleInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityRoleRole> Get(IRequestItem<long> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityRoleRoleGetRequest req = new SecurityRoleRoleGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRoleRoleGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityRoleRole> Update(IRequestItem<ServiceModel.SecurityRoleRole> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityRoleRoleUpdateRequest req = new SecurityRoleRoleUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRoleRoleUpdateResponse>(req);
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
                SecurityRoleRoleDeleteRequest req = new SecurityRoleRoleDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRoleRoleDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceModel.SecurityRoleRole> Query(IRequestItem<IStorageQuery> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityRoleRoleQueryRequest req = new SecurityRoleRoleQueryRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRoleRoleQueryResponse>(req);
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
                SecurityRoleRoleQueryAggregateRequest req = new SecurityRoleRoleQueryAggregateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityRoleRoleQueryAggregateResponse>(req);
            }
        }
    }
}
