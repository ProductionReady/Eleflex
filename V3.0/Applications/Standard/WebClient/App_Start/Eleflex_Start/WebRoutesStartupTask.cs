using System.Web.Routing;
using System.Web.Mvc;
using Eleflex;
using MvcCodeRouting;

namespace WebClient.App_Start.Eleflex_Start
{
    /// <summary>
    /// Represents a startup task for configuring routes in the web application.
    /// </summary>
    public partial class WebRoutesStartupTask : StartupTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebRoutesStartupTask() : base()
        {
            Description = @"This task registers mvc routes used by the web application.";
            Priority = StartupConstants.PRIORITY_CUSTOM;
        }

        /// <summary>
        /// Start processing logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {
            //CONFIGURE MVC CODE ROUTING (ALLOWS EMBEDDED WEB APPLICATION MODULES)
            //Ignore routes
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Set the default controller for MvcCodeRouting
            RouteTable.Routes.MapCodeRoutes(rootController: typeof(WebClient.Controllers.EleflexHomeController));

            //Enable code routing for MvcCodeRouting
            ViewEngines.Engines.EnableCodeRouting();
            ControllerBuilder.Current.EnableCodeRouting();

            //TO VIEW ALL ROUTES AVAILABLE IN APPLICATION FOR MVCCODEROUTING, BROWSE TO http://localhost:16185/routes.axd (replace 16185 with your web application port number)
            //This will only work for local requests, no public access, good for debugging.


            //CONFIGURE ROUTE FOR ACCOUNT MANAGEMENT (Login, logout, change password, etc...)
            RouteTable.Routes.MapCodeRoutes(
               baseRoute: "Account",
               rootController: typeof(Eleflex.Security.Web.Account.Controllers.AccountController),
               settings: new CodeRoutingSettings
               {
                   EnableEmbeddedViews = true,
               }
            );

            //CONFIGURE ROUTE FOR ADMIN LOGGING
            RouteTable.Routes.MapCodeRoutes(
               baseRoute: "Admin/Logging",
               rootController: typeof(Eleflex.Logging.Web.Admin.Controllers.AdminController),
               settings: new CodeRoutingSettings
               {
                   EnableEmbeddedViews = true,
               }
            );

            //CONFIGURE ROUTE FOR ADMIN SECURITY
            RouteTable.Routes.MapCodeRoutes(
               baseRoute: "Admin/Security",
               rootController: typeof(Eleflex.Security.Web.Admin.Controllers.AdminController),
               settings: new CodeRoutingSettings
               {
                   EnableEmbeddedViews = true,
               }
            );

            //CONFIGURE ROUTE FOR ADMIN VERSIONING
            RouteTable.Routes.MapCodeRoutes(
               baseRoute: "Admin/Versioning",
               rootController: typeof(Eleflex.Versioning.Web.Admin.Controllers.AdminController),
               settings: new CodeRoutingSettings
               {
                   EnableEmbeddedViews = true,
               }
            );

            return base.Start(taskOptions);
        }
    }
}
