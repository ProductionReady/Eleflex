#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections.Generic;
using Eleflex;
using Eleflex.Services;
using Eleflex.Storage;
using Eleflex.Security.Message.PermissionCommand;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Security.Message
{
    /// <summary>
    /// Service client for Permissions.
    /// </summary>
    public class PermissionServiceClient : IPermissionServiceClient
    {
        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Permission> Insert(Permission entity)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                PermissionCreateRequest request = new PermissionCreateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<PermissionCreateResponse>(request);
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Permission> Get(Guid id)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                PermissionGetRequest request = new PermissionGetRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<PermissionGetResponse>(request);
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Permission> Update(Permission entity)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                PermissionUpdateRequest request = new PermissionUpdateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<PermissionUpdateResponse>(request);
            }
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponse Delete(Guid id)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                PermissionDeleteRequest request = new PermissionDeleteRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<PermissionDeleteResponse>(request);
            }
        }

        /// <summary>
        /// Query.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItems<Permission> Query(IStorageQuery storageQuery)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                PermissionQueryRequest request = new PermissionQueryRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<PermissionQueryResponse>(request);
            }
        }

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<double> QueryAggregate(IStorageQuery storageQuery)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                PermissionQueryAggregateRequest request = new PermissionQueryAggregateRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<PermissionQueryAggregateResponse>(request);
            }
        }
    }
}
