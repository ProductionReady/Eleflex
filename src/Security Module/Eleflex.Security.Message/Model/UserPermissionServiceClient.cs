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
using Eleflex.Security.Message.UserPermissionCommand;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Security.Message
{
    /// <summary>
    /// Service client for UserPermissions.
    /// </summary>
    public class UserPermissionServiceClient : IUserPermissionServiceClient
    {
        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<UserPermission> Insert(UserPermission entity)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                UserPermissionCreateRequest request = new UserPermissionCreateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<UserPermissionCreateResponse>(request);
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<UserPermission> Get(long id)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                UserPermissionGetRequest request = new UserPermissionGetRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<UserPermissionGetResponse>(request);
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<UserPermission> Update(UserPermission entity)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                UserPermissionUpdateRequest request = new UserPermissionUpdateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<UserPermissionUpdateResponse>(request);
            }
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponse Delete(long id)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                UserPermissionDeleteRequest request = new UserPermissionDeleteRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<UserPermissionDeleteResponse>(request);
            }
        }

        /// <summary>
        /// Query.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItems<UserPermission> Query(IStorageQuery storageQuery)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                UserPermissionQueryRequest request = new UserPermissionQueryRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<UserPermissionQueryResponse>(request);
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
                UserPermissionQueryAggregateRequest request = new UserPermissionQueryAggregateRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<UserPermissionQueryAggregateResponse>(request);
            }
        }
    }
}
