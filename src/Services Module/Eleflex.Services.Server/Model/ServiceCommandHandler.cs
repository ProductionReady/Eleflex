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
using Eleflex;
using Eleflex.Services;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Services.Server
{
    /// <summary>
    /// Abstract class defining a handler to process a service command.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class ServiceCommandHandler<TRequest, TResponse> : IServiceCommandHandler
        where TRequest : ServiceCommandRequest
        where TResponse : ServiceCommandResponse, new()
    {

        /// <summary>
        /// Handle a service command
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual ServiceCommandResponse SendServiceCommand(ServiceCommandRequest request)
        {            
            TResponse response = ServiceLocator.Current.GetInstance<TResponse>();
            IUnitOfWork uow = ServiceLocator.Current.GetInstance<IUnitOfWork>();
            try
            {
                Execute((TRequest)request, (TResponse)response);
                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();                
                if (response != null)
                    response.ResponseStatus.AddError(ex.Message, ServicesConstants.ERROR_SYSTEM_GENERAL);

                //Log exceptions for non-business exceptions since they are not managed errors
                if (!(ex is IEleflexException))
                    Common.Logging.LogManager.GetLogger("Eleflex.Services.Server.ServiceCommandHandler").Error(request, ex);
            }            
            return response;
        }


        /// <summary>
        /// Execute a service command.
        /// </summary>
        /// <param name="request"></param>
        public abstract void Execute(TRequest request, TResponse response);     

    }
}
