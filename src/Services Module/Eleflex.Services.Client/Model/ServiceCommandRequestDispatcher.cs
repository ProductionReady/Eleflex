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
using System.Linq;
using System.ServiceModel;
using Eleflex.Services;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Services.Client
{
    /// <summary>
    /// Service proxy abstraction used to call the service. Exceptions are trapped and a valid response object is returned with a context containing errors.
    /// </summary>
    public class ServiceCommandRequestDispatcher : ClientBase<IServiceCommandHandler>, IServiceCommandHandler
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceCommandRequestDispatcher()
            : base(Eleflex.Services.ServicesConstants.SERVICE_ENDPOINT_NAME_DEFAULT) 
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint"></param>
        public ServiceCommandRequestDispatcher(string endpoint)
            : base(endpoint) 
        { }

        /// <summary>
        /// Send the service command.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual ServiceCommandResponse SendServiceCommand(ServiceCommandRequest request)
        {
            return Channel.SendServiceCommand(request);
        }

        /// <summary>
        /// Send service command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual T SendServiceCommand<T>(ServiceCommandRequest request) where T : class, IServiceCommandResponse
        {
            return Channel.SendServiceCommand(request) as T;
        }
    }
}
