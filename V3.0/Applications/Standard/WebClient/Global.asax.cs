using System;
using System.Web;
using Eleflex;

namespace WebClient
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Called when the application starts.
        /// </summary>
        protected void Application_Start()
        {
            //Startup the system
            var success = Eleflex.SystemStartupShutdown.Start(null);
        }

        /// <summary>
        /// Called when the application terminates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End()
        {
            //Shutdown the system
            Eleflex.SystemStartupShutdown.Stop(null);
        }

        /// <summary>
        /// Called when an error happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            if (sender is System.Web.HttpApplication)
            {
                HttpApplication application = (HttpApplication)sender;
                Exception exception = application.Server.GetLastError();
                Logger.Current.Error<MvcApplication>("Web Application Error", exception);
            }
            else
            {
                Logger.Current.Error<MvcApplication>("Global.asax Error Type " + sender != null ? sender.GetType().FullName : string.Empty + " ToString() is "
                    + sender != null ? sender.ToString() : string.Empty);
            }
            HttpContext.Current.Response.Redirect(@"/Error");
        }


        /// <summary>
        /// Called when the request terminates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            //Commit and Dispose any left over work used in this request
            var uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
            uow.Commit();
            uow.Dispose();

            //Cleanup structuremap
            StructureMap.Web.Pipeline.HttpContextLifecycle.DisposeAndClearAll();
        }
    }
}
