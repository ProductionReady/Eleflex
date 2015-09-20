using System;
using System.ServiceModel;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Service proxy abstraction used to call the service. Exceptions are trapped and a valid response object is returned with a context containing errors.
    /// </summary>
    public partial class WCFCommandRequestDispatcher : ClientBase<IWCFCommand>, IWCFCommand, IDisposable
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public WCFCommandRequestDispatcher()
            : base(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT) 
        {
            Name = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint"></param>
        public WCFCommandRequestDispatcher(string endpoint)
            : base(endpoint) 
        {
            Name = Guid.NewGuid().ToString();
        }

        public string Name;
        /// <summary>
        /// Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            try
            {
                Abort();
            }
            catch { }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Send the service command.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual Response ExecuteServiceCommand(Request request)
        {
            try
            { 
                return Channel.ExecuteServiceCommand(request);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<WCFCommandRequestDispatcher>(ex);
                Response resp = new Response();
                resp.AddMessage(true, MessageConstants.ERROR_SERVICES_CODE, MessageConstants.ERROR_SERVICES_TEXT);
                return resp;
            }
        }

        /// <summary>
        /// Execute service command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual T ExecuteServiceCommand<T>(Request request) where T : class, IResponse, new()
        {
            try
            {
                return Channel.ExecuteServiceCommand(request) as T;
            }
            catch (Exception ex)
            {
                Logger.Current.Error<WCFCommandRequestDispatcher>(ex);
                T resp = new T();
                resp.AddMessage(true, MessageConstants.ERROR_SERVICES_CODE, MessageConstants.ERROR_SERVICES_TEXT);
                return resp;
            }
        }
    }
}
