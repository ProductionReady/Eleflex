#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Web.Optimization;
using Bootstrap.Extensions.StartupTasks;
using Eleflex.Web;

namespace $rootnamespace$.App_Start.Eleflex_Start
{
    /// <summary>
    /// Defines configurations for bundling.
    /// </summary>
    [Task]
    public class BundleConfig : IStartupTask
    {        
        /// <summary>
        /// Run.
        /// </summary>
        public void Run()
        {
            //JQuery Scripts
            BundleCollection bundles = BundleTable.Bundles;
            bundles.Add(new ScriptBundle("~/bundles/jqueryEleflex").Include(
                        "~/Scripts/jquery-{version}.js"));

            //JQuery UI Scripts
            bundles.Add(new ScriptBundle("~/bundles/jqueryuiEleflex").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            //JQuery Validation Scripts
            bundles.Add(new ScriptBundle("~/bundles/jqueryvalEleflex").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            //JQuery Data Tables Scripts (tables)
            bundles.Add(new ScriptBundle("~/bundles/datatablejsEleflex").Include(
            "~/Scripts/DataTables-1.10.4/jquery.dataTables.js"));

            //JQuery Chosen Scripts (select choosers)
            bundles.Add(new ScriptBundle("~/bundles/chosenjsEleflex").Include(
            "~/Scripts/chosen.jquery.js"));

            //Bootstrap Scripts
            bundles.Add(new ScriptBundle("~/bundles/bootstrapEleflex").Include(
            "~/Scripts/bootstrap.js"));

            //Knockout Scripts
            bundles.Add(new ScriptBundle("~/bundles/knockoutjsEleflex").Include(
            "~/Scripts/knockout-{version}.js"));

            //Moment Scripts (req by datetimepicker)
            bundles.Add(new ScriptBundle("~/bundles/momentjsEleflex").Include(
            "~/Scripts/moment.js"));

            //Moderizer Scripts
            bundles.Add(new ScriptBundle("~/bundles/modernizrEleflex").Include(
            "~/Scripts/modernizr-*"));
            
            //Bootstrap datetimepicker Scripts (date/time choosers)
            bundles.Add(new ScriptBundle("~/bundles/datetimepickerjsEleflex").Include(
            "~/Scripts/bootstrap-datetimepicker.js"));

            //Eleflex Scripts
            bundles.Add(new ScriptBundle("~/bundles/jsEleflex").Include(
            "~/Scripts/Eleflex.js"));



            //JQuery Data Tables CSS
            bundles.Add(new StyleBundle("~/Content/DataTables-1.10.4/cssEleflex").Include(
                        "~/Content/DataTables-1.10.4/css/jquery.dataTables.css"));

            //JQuery Theme CSS
            bundles.Add(new StyleBundle("~/Content/themes/base/cssEleflex").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            //Chosen select pickers CSS (Default css removed for bootstrap.chosen alternate style)
            //bundles.Add(new StyleBundle("~/Content/chosenEleflex").Include(
            //    "~/Content/chosen.css"));
            

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
            bundles.Add(new StyleBundle("~/Content/datetimepickerEleflex").Include(
                "~/Content/bootstrap-datetimepicker.css"));

            //Eleflex CSS
            bundles.Add(new StyleBundle("~/Content/cssEleflex").Include(
                "~/Content/Eleflex.css",
                "~/Content/EleflexTheme.css"));

        }

        /// <summary>
        /// Reset.
        /// </summary>
        public void Reset()
        {
        }
    }
}