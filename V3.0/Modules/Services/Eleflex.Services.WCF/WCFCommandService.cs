using System;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Service used as entry point for all ELEFLEX Modules and exposed services in the platform.
    /// </summary>
    public partial class WCFCommandService : IWCFCommand
    {        

        /// <summary>
        /// Execute a command.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual Response ExecuteServiceCommand(Request request)
        {
            if (request == null)
                return null;

            //Find the service command handler by the request type
            IWCFCommand handler = WCFCommandRegistry.Current.GetCommand(request.GetType());
            if (handler != null)
                return handler.ExecuteServiceCommand(request); //Process request
            else
            {
                Logger.Current.Error<WCFCommandService>("No WCFCommand defined for: " + request != null ? request.GetType().ToString() : string.Empty);
                Response resp = new Response();
                resp.AddMessage(true, MessageConstants.ERROR_SERVICES_CODE, "No command defined for request");
                return resp;
            }
        }
    }
}
