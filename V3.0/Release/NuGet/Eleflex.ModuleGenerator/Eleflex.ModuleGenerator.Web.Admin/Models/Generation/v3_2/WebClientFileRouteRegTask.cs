using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.ModuleGenerator.Web.Admin.Models.Generation.v3_2
{
    class WebClientFileRouteRegTask : IGenerate
    {

        string _namespaceRoot;
        string _namespaceName;
        string _moduleName;
        string _namespaceWebAdmin;

        public WebClientFileRouteRegTask(string namespaceRoot, string moduleName, string namespaceName, string namespaceWebAdmin)
        {
            _namespaceRoot = namespaceRoot;
            _moduleName = moduleName;
            _namespaceName = namespaceName;
            _namespaceWebAdmin = namespaceWebAdmin;
        }

        public string Generate()
        {


            return @"using System.Web.Routing;
using Eleflex;
using MvcCodeRouting;

namespace " + _namespaceName + @"
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
            Description = @""This task registers mvc routes used for the " + _namespaceRoot + @" Module."";
            Priority = StartupConstants.PRIORITY_CUSTOM;
        }

        /// <summary>
        /// Start processing logic.
        /// </summary>
        /// <param name=""taskOptions""></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {
            
            RouteTable.Routes.MapCodeRoutes(
               baseRoute: ""Admin/" + _moduleName + @""",
               rootController: typeof(" + _namespaceWebAdmin + @".Controllers.AdminController),
               settings: new CodeRoutingSettings
               {
                   EnableEmbeddedViews = true,
               }
            );

            return base.Start(taskOptions);
        }
    }
}
";
        }
    }
}
