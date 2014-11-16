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

namespace Eleflex.Services.Server
{
    /// <summary>
    /// Service used as entry point for all ELEFLEX Modules and exposed services in the platform.
    /// </summary>
    public class ServiceCommandService : IServiceCommandHandler
    {        

        /// <summary>
        /// Send a command.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual ServiceCommandResponse SendServiceCommand(ServiceCommandRequest request)
        {
            if (request == null)
                return null;

            //Find the service command handler by the request type
            IServiceCommandHandler handler = ServiceCommandHandlerProvider.GetHandler(request.GetType());
            if (handler != null)
                return handler.SendServiceCommand(request); //Process request
            else
            {
                Common.Logging.LogManager.GetCurrentClassLogger().Error("No ServiceCommandHandler defined for: " + request.GetType().ToString());
                throw new Exception("No ServiceCommandHandler defined for: " + request.GetType().ToString());
            }
        }
    }
}
