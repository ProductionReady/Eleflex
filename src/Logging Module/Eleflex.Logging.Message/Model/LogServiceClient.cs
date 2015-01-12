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
using Eleflex.Logging.Message.LogCommand;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Logging.Message
{
    /// <summary>
    /// Service client for logs.
    /// </summary>
    public class LogServiceClient : ILogServiceClient
    {
        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Log> Insert(Log entity)
        {
            using (ILoggingRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogCreateRequest request = new LogCreateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<LogCreateResponse>(request);
            }
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Log> Get(long id)
        {
            using (ILoggingRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogGetRequest request = new LogGetRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<LogGetResponse>(request);
            }            
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<Log> Update(Log entity)
        {
            using (ILoggingRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogUpdateRequest request = new LogUpdateRequest();
                request.Item = entity;
                return dispatcher.SendServiceCommand<LogUpdateResponse>(request);
            }
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IServiceCommandResponse Delete(long id)
        {
            using (ILoggingRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogDeleteRequest request = new LogDeleteRequest();
                request.Item = id;
                return dispatcher.SendServiceCommand<LogDeleteResponse>(request);
            }
        }

        /// <summary>
        /// Query.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItems<Log> Query(IStorageQuery storageQuery)
        {
            using (ILoggingRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogQueryRequest request = new LogQueryRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<LogQueryResponse>(request);
            }
        }

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public IServiceCommandResponseItem<double> QueryAggregate(IStorageQuery storageQuery)
        {
            using (ILoggingRequestDispatcher dispatcher = ServiceLocator.Current.GetInstance<ILoggingRequestDispatcher>())
            {
                LogQueryAggregateRequest request = new LogQueryAggregateRequest();
                if (storageQuery != null)
                {
                    request.Filters = storageQuery.Filters;
                    request.NumberPerPage = storageQuery.NumberPerPage;
                    request.StartPage = storageQuery.StartPage;
                }
                return dispatcher.SendServiceCommand<LogQueryAggregateResponse>(request);
            }
        }
    }
}
