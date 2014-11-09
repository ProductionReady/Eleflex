#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
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
using Eleflex.Services.Server;
using Eleflex.Logging;
using Eleflex.Logging.Message.LogCommand;
using DomainModel = Eleflex.Logging;
using ServiceModel = Eleflex.Logging.Message;

namespace Eleflex.Logging.Service.LogCommand
{
    /// <summary>
    /// Service command to create a log.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(LogCreateRequest))]
    public class LogCreate : ServiceCommandHandler<LogCreateRequest, LogCreateResponse>
    {
        private readonly ILogRepository _logRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logRepository"></param>
        public LogCreate(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void Execute(LogCreateRequest request, LogCreateResponse response)
        {
            DomainModel.Log item = new DomainModel.Log();
            item.ChangeApplication(request.Item.Application);
            item.ChangeCreateDate(request.Item.CreateDate);
            item.ChangeIsError(request.Item.IsError);
            item.ChangeLogKey(request.Item.LogKey);
            item.ChangeMessage(request.Item.Message);
            item.ChangeServer(request.Item.Server);
            item.ChangeSeverity(request.Item.Severity);            
            item = _logRepository.Insert(item);
            response.Item = AutoMapper.Mapper.Map<ServiceModel.Log>(item);
        }
    }
}
