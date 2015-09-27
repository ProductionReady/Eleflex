using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Web;
using Eleflex;

namespace WebServer
{
    public class MvcApplication : System.Web.HttpApplication
    {

        /// <summary>
        /// Called when the application starts.
        /// </summary>
        protected void Application_Start()
        {
            //Load as many of the assemblies/custom modules into the application domain as possible
            LoadAllReferencedAssemblies();

            //Start the system
            SystemStartupShutdown startup = new SystemStartupShutdown();
            startup.Start(null);
        }


        /// <summary>
        /// Load all assemblies.
        /// </summary>
        protected void LoadAllReferencedAssemblies()
        {
            List<string> allFiles = new List<string>();
            string path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin");
            allFiles.AddRange(Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly));
            allFiles.AddRange(Directory.GetFiles(System.AppDomain.CurrentDomain.DynamicDirectory, "*.dll", SearchOption.TopDirectoryOnly));            
            foreach (string dll in allFiles)
            {
                try
                {
                    Assembly loadedAssembly = Assembly.LoadFile(dll);
                }
                catch { }
            }
        }

        /// <summary>
        /// Called when the application terminates.
        /// </summary>
        protected void Application_End()
        {
            //Shutdown the system
            SystemStartupShutdown shutdown = new SystemStartupShutdown();
            shutdown.Stop(null);
        }

        /// <summary>
        /// Called when an error happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                if (sender is System.Web.HttpApplication)
                {
                    HttpApplication application = (HttpApplication)sender;
                    Exception exception = application.Server.GetLastError();
                    Logger.Current.Error<MvcApplication>("Web Application Error", exception);
                }
                else
                {
                    string error = "Global.asax Error Type: ";
                    error += sender != null ? sender.GetType().FullName : string.Empty;
                    error += " ToString() is ";
                    error += sender != null ? sender.ToString() : string.Empty;
                    Logger.Current.Error<MvcApplication>(error);
                }
                HttpContext.Current.Response.Redirect(@"/Error");
            }
            catch { }
        }


        /// <summary>
        /// Called when the request terminates.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            try
            {
                //Commit and Dispose any left over work used in this request
                var uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
                if (uow != null)
                {
                    uow.Commit();
                    uow.Dispose();
                }

                //Cleanup structuremap
                StructureMap.Web.Pipeline.HttpContextLifecycle.DisposeAndClearAll();
            }
            catch { }
        }
    }
}
