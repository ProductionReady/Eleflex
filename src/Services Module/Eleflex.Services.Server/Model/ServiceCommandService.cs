using System;
using System.Collections.Generic;

namespace Eleflex.Services.Server
{
    public class ServiceCommandService : IServiceCommandHandler
    {        
        public virtual ServiceCommandResponse SendServiceCommand(ServiceCommandRequest request)
        {
            if (request == null)
                return null;
            IServiceCommandHandler handler = ServiceCommandHandlerProvider.GetHandler(request.GetType());
            if (handler != null)
                return handler.SendServiceCommand(request);
            else
            {
                Common.Logging.LogManager.GetCurrentClassLogger().Error("No ServiceCommandHandler defined for: " + request.GetType().ToString());
                throw new Exception("No ServiceCommandHandler defined for: " + request.GetType().ToString());
            }
        }
    }
}
