using System;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Abstract class defining a handler to process a service command.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract partial class WCFCommand<TRequest, TResponse> : IWCFCommand
        where TRequest : Request
        where TResponse : Response, new()
    {

        /// <summary>
        /// Execute a service command.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual Response ExecuteServiceCommand(Request request)
        {            
            TResponse response = ObjectLocator.Current.GetInstance<TResponse>();
            IStorageContextUnitOfWork uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
            try
            {
                Execute((TRequest)request, (TResponse)response);
                uow.Commit();
            }
            catch (System.Security.SecurityException)
            {
                uow.Rollback();
                response.AddMessage(true, MessageConstants.ERROR_SECURITY_CODE, MessageConstants.ERROR_SECURITY_TEXT);
            }
            catch (Exception ex)
            {
                uow.Rollback();                

                if (ex is IEleflexException)
                    response.CopyResponse(ex as IEleflexException);
                else
                {
                    Logger.Current.Error<WCFCommand<TRequest, TResponse>>(ex);
                    response.AddMessage(true, MessageConstants.ERROR_SYSTEM_CODE, MessageConstants.ERROR_SYSTEM_TEXT);                    
                }
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
