using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ServiceModel = Eleflex;

namespace Eleflex.Logging.Web.Admin.Models
{
    /// <summary>
    /// List model.
    /// </summary>
    public class ListModel
    {
        /// <summary>
        /// Search error
        /// </summary>
        public bool? IsError { get; set; }

        /// <summary>
        /// Search severity.
        /// </summary>
        public string[] Severity { get; set; }

        /// <summary>
        /// Search message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Search source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Search Exception.
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// Search max records.
        /// </summary>
        public int? MaxRecords { get; set; }

        /// <summary>
        /// Search create date begin.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Search create date end.
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Is error selecton items.
        /// </summary>
        public List<SelectListItem> ErrorSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "",Value=""},
                    new SelectListItem(){Text = "True",Value="true"},
                    new SelectListItem(){Text = "False",Value="false"},                    
                };
            }
        }

        /// <summary>
        /// Severity selecton items.
        /// </summary>
        public List<SelectListItem> SeveritySelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "",Value=""},
                    new SelectListItem(){Text = "Error",Value="error"},
                    new SelectListItem(){Text = "Fatal",Value="fatal"},
                    new SelectListItem(){Text = "Debug",Value="debug"},
                    new SelectListItem(){Text = "Info",Value="info"},
                    new SelectListItem(){Text = "Warn",Value="warn"},
                };
            }
        }

        /// <summary>
        /// Items displayed.
        /// </summary>
        public IList<ServiceModel.LogMessage> Items { get; set; }

    }
}