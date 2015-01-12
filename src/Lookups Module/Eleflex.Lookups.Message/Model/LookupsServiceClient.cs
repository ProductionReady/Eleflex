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
using Eleflex.Lookups.Message;
using Eleflex.Lookups.Message.LookupCommand;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Lookups.Message
{
    /// <summary>
    /// Service client for Lookups.
    /// </summary>
    public class LookupsServiceClient : ILookupsServiceClient
    {
        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Lookup> Insert(Lookup entity)
        {
            using (ILookupsRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupCreateRequest request = new LookupCreateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<LookupCreateResponse>(request);
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Lookup> Get(Guid id)
        {
            using (ILookupsRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupGetRequest request = new LookupGetRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<LookupGetResponse>(request);
            }
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Lookup> Update(Lookup entity)
        {
            using (ILookupsRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupUpdateRequest request = new LookupUpdateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<LookupUpdateResponse>(request);
            }
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponse Delete(Guid id)
        {
            using (ILookupsRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupDeleteRequest request = new LookupDeleteRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<LookupDeleteResponse>(request);
            }
        }

        /// <summary>
        /// Query a list of items.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItems<Lookup> Query(IStorageQuery storageQuery)
        {
            using (ILookupsRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupQueryRequest request = new LookupQueryRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<LookupQueryResponse>(request);
            }
        }

        /// <summary>
        /// Query a list of items for aggregate.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<double> QueryAggregate(IStorageQuery storageQuery)
        {
            using (ILookupsRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupQueryAggregateRequest request = new LookupQueryAggregateRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<LookupQueryAggregateResponse>(request);
            }
        }

        /// <summary>
        /// Get categories.
        /// </summary>
        /// <returns></returns>
        public IServiceCommandResponseItems<Lookup> GetCategories()
        {
            using (ILookupsRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupGetCategoriesRequest request = new LookupGetCategoriesRequest();
                return dispatcher.SendServiceCommand<LookupGetCategoriesResponse>(request);
            }
        }

        /// <summary>
        /// Get lookups by category key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IServiceCommandResponseItems<Lookup> GetLookupsForCategoryKey(Guid key)
        {
            using (ILookupsRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupGetLookupsForCategoryCodeRequest request = new LookupGetLookupsForCategoryCodeRequest();
                request.Item = key;
                return dispatcher.SendServiceCommand<LookupGetLookupsForCategoryCodeResponse>(request);
            }
        }

        /// <summary>
        /// Get lookups by category name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IServiceCommandResponseItems<Lookup> GetLookupsForCategoryName(string name)
        {
            using (ILookupsRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILookupsRequestDispatcher>())
            {
                LookupGetLookupsForCategoryNameRequest request = new LookupGetLookupsForCategoryNameRequest();
                request.Item = name;
                return dispatcher.SendServiceCommand<LookupGetLookupsForCategoryNameResponse>(request);
            }
        }
    }
}
