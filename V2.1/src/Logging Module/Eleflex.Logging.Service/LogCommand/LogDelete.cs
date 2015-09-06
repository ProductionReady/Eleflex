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
using System.Security.Permissions;
using Eleflex.Services.Server;
using Eleflex.Logging;
using Eleflex.Logging.Message.LogCommand;
using DomainModel = Eleflex.Logging;
using ServiceModel = Eleflex.Logging.Message;

namespace Eleflex.Logging.Service.LogCommand
{
    /// <summary>
    /// Service command to delete a log.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(LogDeleteRequest))]
    public class LogDelete : ServiceCommandHandler<LogDeleteRequest, LogDeleteResponse>
    {
        private readonly ILogRepository _logRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logRepository"></param>
        public LogDelete(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(LogDeleteRequest request, LogDeleteResponse response)
        {
            _logRepository.Delete(request.Item);
        }
    }
}
