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
using Eleflex.Security.Message.RoleCommand;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Security.Message
{
    /// <summary>
    /// Service client for Roles.
    /// </summary>
    public class RoleServiceClient : IRoleServiceClient
    {
        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Role> Insert(Role entity)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                RoleCreateRequest request = new RoleCreateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<RoleCreateResponse>(request);
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Role> Get(Guid id)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                RoleGetRequest request = new RoleGetRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<RoleGetResponse>(request);
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Role> Update(Role entity)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                RoleUpdateRequest request = new RoleUpdateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<RoleUpdateResponse>(request);
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
                RoleDeleteRequest request = new RoleDeleteRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<RoleDeleteResponse>(request);
            }
        }

        /// <summary>
        /// Query.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItems<Role> Query(IStorageQuery storageQuery)
        {
            using (ISecurityRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ISecurityRequestDispatcher>())
            {
                RoleQueryRequest request = new RoleQueryRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<RoleQueryResponse>(request);
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
                RoleQueryAggregateRequest request = new RoleQueryAggregateRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<RoleQueryAggregateResponse>(request);
            }
        }
    }
}
