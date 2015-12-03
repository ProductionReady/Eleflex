using System.Web.Routing;
using Eleflex;
using MvcCodeRouting;

namespace $rootnamespace$.App_Start.Eleflex_Start.EleflexLookups
{
    /// <summary>
    /// Represents a startup task for configuring routes in the web application.
    /// </summary>
    public partial class WebClientRoutesStartupTask : StartupTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebClientRoutesStartupTask() : base()
        {
            Description = @"This task registers mvc routes used for the ELEFLEX Lookups Module.";
            Priority = StartupConstants.PRIORITY_CUSTOM;
        }

        /// <summary>
        /// Start processing logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {

            //CONFIGURE ROUTE FOR ADMIN/LOOKUPS
            RouteTable.Routes.MapCodeRoutes(
               baseRoute: "Admin/Lookups",
               rootController: typeof(Eleflex.Lookups.Web.Admin.Controllers.AdminController),
               settings: new CodeRoutingSettings
               {
                   EnableEmbeddedViews = true,
               }
            );
            
            return base.Start(taskOptions);
        }
    }
}
