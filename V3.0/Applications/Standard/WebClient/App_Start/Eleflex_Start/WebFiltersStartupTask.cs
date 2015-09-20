using System.Web.Mvc;
using Eleflex;

namespace WebClient.App_Start.Eleflex_Start
{
    /// <summary>
    /// Represents a startup task for configuring web application filters.
    /// </summary>
    public partial class WebFiltersStartupTask : StartupTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebFiltersStartupTask() : base()
        {
            Description = @"This task registers filters used by the web application.";
            Priority = StartupConstants.PRIORITY_CUSTOM;
        }

        /// <summary>
        /// Start processing logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());

            return base.Start(taskOptions);
        }
    }
}
