using System.Web.Optimization;
using Eleflex;

namespace $rootnamespace$.App_Start.Eleflex_Start
{
    /// <summary>
    /// Represents a startup task for configuring web application bundles.
    /// </summary>
    public partial class WebBundlesStartupTask : StartupTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebBundlesStartupTask() : base()
        {
            Description = @"This task registers bundles used by the web application.";
            Priority = StartupConstants.PRIORITY_CUSTOM;
        }

        /// <summary>
        /// Start processing logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            BundleTable.Bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            BundleTable.Bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            //OLD STUFF
            //JQuery Scripts
            BundleCollection bundles = BundleTable.Bundles;
            bundles.Add(new ScriptBundle("~/bundles/jqueryEleflex").Include(
                        "~/Scripts/jquery-{version}.js"));

            //JQuery UI Scripts
            bundles.Add(new ScriptBundle("~/bundles/jqueryuiEleflex").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            //JQuery Validation Scripts
            bundles.Add(new ScriptBundle("~/bundles/jqueryvalEleflex").Include(
                        "~/Scripts/jquery.validate*"));

            //JQuery Data Tables Scripts (tables)
            bundles.Add(new ScriptBundle("~/bundles/datatablejsEleflex").Include(
            "~/Scripts/DataTables/jquery.dataTables.js"));

            //JQuery Chosen Scripts (select choosers)
            bundles.Add(new ScriptBundle("~/bundles/chosenjsEleflex").Include(
            "~/Scripts/chosen.jquery.js"));

            //Moderizer Scripts
            bundles.Add(new ScriptBundle("~/bundles/modernizrEleflex").Include(
            "~/Scripts/modernizr-*"));

            //Bootstrap Scripts
            bundles.Add(new ScriptBundle("~/bundles/bootstrapEleflex").Include(
            "~/Scripts/bootstrap.js",
            "~/Scripts/respond.js"));

            //Moment Scripts (req by datetimepicker)
            bundles.Add(new ScriptBundle("~/bundles/momentjsEleflex").Include(
            "~/Scripts/moment.js"));            

            //Bootstrap datetimepicker Scripts (date/time choosers)
            bundles.Add(new ScriptBundle("~/bundles/datetimepickerjsEleflex").Include(
            "~/Scripts/smalot-datetimepicker/bootstrap-datetimepicker.js"));

            //Eleflex Scripts
            bundles.Add(new ScriptBundle("~/bundles/jsEleflex").Include(
            "~/Scripts/Eleflex.js"));



            //JQuery Data Tables CSS
            bundles.Add(new StyleBundle("~/Content/DataTables/cssEleflex").Include(
                        "~/Content/DataTables/css/jquery.dataTables.css"));

            //JQuery UI Theme CSS
            bundles.Add(new StyleBundle("~/Content/themes/base/cssEleflex").Include(
                        "~/Content/themes/base/*.css"));

            //Bootstrap Chosen select pickers CSS
            bundles.Add(new StyleBundle("~/Content/bootstrapchosenEleflex").Include(
                "~/Content/bootstrap-chosen.css"));

            //Font awesome CSS
            bundles.Add(new StyleBundle("~/Content/fontawesomeEleflex").Include(
                "~/Content/font-awesome.css"));

            //Bootstrap CSS
            bundles.Add(new StyleBundle("~/Content/bootstrapEleflex").Include(
                "~/Content/bootstrap.css"));

            //Bootstrap datetimepicker CSS
            bundles.Add(new StyleBundle("~/Content/smalot-datetimepicker/datetimepickerEleflex").Include(
                "~/Content/smalot-datetimepicker/bootstrap-datetimepicker.css"));

            //Eleflex CSS
            bundles.Add(new StyleBundle("~/Content/cssEleflex").Include(
                "~/Content/Eleflex.css",
                "~/Content/EleflexTheme.css"));

            return base.Start(taskOptions);
        }
    }
}
