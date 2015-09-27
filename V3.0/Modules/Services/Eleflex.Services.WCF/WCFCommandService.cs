using System;
using System.Linq;

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
            //Validate request
            Response response = new Response();
            if (request == null)
            {
                response.AddMessage(true, MessageConstants.ERROR_SERVICES_CODE, "No command defined for request");
                return response;
            }

            //Find the service command handler by the request type
            IWCFCommand handler = WCFCommandRegistry.Current.GetCommand(request.GetType());
            if (handler != null)
            {
                try//Added in case inheritors don't provide fault protection
                { 
                    return handler.ExecuteServiceCommand(request); //Process request
                }
                catch(Exception ex)
                {
                    Logger.Current.Error<WCFCommandService>("Error executing request " + request.GetType().ToString(), ex);
                    response.AddMessage(true, MessageConstants.ERROR_SYSTEM_CODE, MessageConstants.ERROR_SYSTEM_TEXT);
                    return response;
                }
            }
            else
            {
                Logger.Current.Debug<WCFCommandService>("No WCFCommand defined for: " + request.GetType().ToString());                    
            }            

            response.AddMessage(true, MessageConstants.ERROR_SERVICES_CODE, "No command defined for request");
            return response;
        }
    }
}
