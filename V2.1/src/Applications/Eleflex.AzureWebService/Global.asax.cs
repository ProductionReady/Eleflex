using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Eleflex.AzureWebService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            //Log shutdown
            Common.Logging.LogManager.GetLogger(typeof(HttpApplication)).Info("Application Shutdown");

            System.Threading.Thread.Sleep(1000);

            //Dispose the logger
            ((IDisposable)Common.Logging.LogManager.Adapter).Dispose();

            //Dispose container
            ((IDisposable)Bootstrap.Bootstrapper.Container).Dispose();  
        }
    }
}