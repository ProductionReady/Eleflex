using System;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service client.
    /// </summary>
    public partial class SecurityUserClaimServiceRepository : ISecurityUserClaimServiceRepository
    {

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityUserClaim> Insert(IRequestItem<ServiceModel.SecurityUserClaim> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserClaimInsertRequest req = new SecurityUserClaimInsertRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserClaimInsertResponse>(req);
            }
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityUserClaim> Get(IRequestItem<long> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserClaimGetRequest req = new SecurityUserClaimGetRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserClaimGetResponse>(req);
            }
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<ServiceModel.SecurityUserClaim> Update(IRequestItem<ServiceModel.SecurityUserClaim> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserClaimUpdateRequest req = new SecurityUserClaimUpdateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserClaimUpdateResponse>(req);
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
                SecurityUserClaimDeleteRequest req = new SecurityUserClaimDeleteRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserClaimDeleteResponse>(req);
            }
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<ServiceModel.SecurityUserClaim> Query(IRequestItem<IStorageQuery> request)
        {
            using (ISecurityRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                SecurityUserClaimQueryRequest req = new SecurityUserClaimQueryRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserClaimQueryResponse>(req);
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
                SecurityUserClaimQueryAggregateRequest req = new SecurityUserClaimQueryAggregateRequest();
                req.Item = request.Item;
                return dispatcher.ExecuteServiceCommand<SecurityUserClaimQueryAggregateResponse>(req);
            }
        }
    }
}
