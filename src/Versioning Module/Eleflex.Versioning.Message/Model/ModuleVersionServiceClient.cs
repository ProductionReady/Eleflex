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
using Eleflex.Versioning.Message;
using Eleflex.Versioning.Message.ModuleVersionCommand;
using Microsoft.Practices.ServiceLocation;
using ServiceModel = Eleflex.Versioning.Message;

namespace Eleflex.Versioning.Message.Repository
{
    /// <summary>
    /// Service client to access module version information.
    /// </summary>
    public class ModuleVersionServiceClient : IModuleVersionServiceClient
    {
        /// <summary>
        /// Inser.t
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<ServiceModel.ModuleVersion> Insert(ServiceModel.ModuleVersion entity)
        {
            using (IVersioningRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleVersionCreateRequest request = new ModuleVersionCreateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<ModuleVersionCreateResponse>(request);
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<ServiceModel.ModuleVersion> Get(Guid key)
        {
            using (IVersioningRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleVersionGetRequest request = new ModuleVersionGetRequest();
                request.Item = key;
                return dispatcher.SendServiceCommand<ModuleVersionGetResponse>(request);
            }            
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<ServiceModel.ModuleVersion> Update(ServiceModel.ModuleVersion entity)
        {
            using (IVersioningRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleVersionUpdateRequest request = new ModuleVersionUpdateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<ModuleVersionUpdateResponse>(request);
            }
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IServiceCommandResponse Delete(Guid key)
        {
            using (IVersioningRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleVersionDeleteRequest request = new ModuleVersionDeleteRequest();
                request.Item = key;
                return dispatcher.SendServiceCommand<ModuleVersionDeleteResponse>(request);
            }
        }

        /// <summary>
        /// Query.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItems<ServiceModel.ModuleVersion> Query(IStorageQuery storageQuery)
        {
            using (IVersioningRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleVersionQueryRequest request = new ModuleVersionQueryRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<ModuleVersionQueryResponse>(request);
            }
        }

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<double> QueryAggregate(IStorageQuery storageQuery)
        {
            using (IVersioningRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<IVersioningRequestDispatcher>())
            {
                ModuleVersionQueryAggregateRequest request = new ModuleVersionQueryAggregateRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<ModuleVersionQueryAggregateResponse>(request);
            }
        }

    }
}
