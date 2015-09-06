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
using Eleflex.Security.Message.UserRoleCommand;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Security.Message
{
    /// <summary>
    /// Service client for UserRoles.
    /// </summary>
    public class UserRoleServiceClient : IUserRoleServiceClient
    {
        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<UserRole> Insert(UserRole entity)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                UserRoleCreateRequest request = new UserRoleCreateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<UserRoleCreateResponse>(request);
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<UserRole> Get(long id)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                UserRoleGetRequest request = new UserRoleGetRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<UserRoleGetResponse>(request);
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<UserRole> Update(UserRole entity)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                UserRoleUpdateRequest request = new UserRoleUpdateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<UserRoleUpdateResponse>(request);
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
                UserRoleDeleteRequest request = new UserRoleDeleteRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<UserRoleDeleteResponse>(request);
            }
        }

        /// <summary>
        /// Query.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItems<UserRole> Query(IStorageQuery storageQuery)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                UserRoleQueryRequest request = new UserRoleQueryRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<UserRoleQueryResponse>(request);
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
                UserRoleQueryAggregateRequest request = new UserRoleQueryAggregateRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<UserRoleQueryAggregateResponse>(request);
            }
        }
    }
}
