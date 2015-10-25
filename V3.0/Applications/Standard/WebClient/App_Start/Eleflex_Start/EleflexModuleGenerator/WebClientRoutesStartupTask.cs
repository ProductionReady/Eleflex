using System.Web.Routing;
using Eleflex;
using MvcCodeRouting;

namespace WebClient.App_Start.Eleflex_Start.EleflexModuleGenerator
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
            Description = @"This task registers mvc routes used for the ELEFLEX Module Generator Module.";
            Priority = StartupConstants.PRIORITY_CUSTOM;
        }

        /// <summary>
        /// Start processing logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {

            //CONFIGURE ROUTE FOR ADMIN/ModuleGenerator
            RouteTable.Routes.MapCodeRoutes(
               baseRoute: "Admin/ModuleGenerator",
               rootController: typeof(Eleflex.ModuleGenerator.Web.Admin.Controllers.AdminController),
               settings: new CodeRoutingSettings
               {
                   EnableEmbeddedViews = true,
               }
            );

            return base.Start(taskOptions);
        }
    }
}
