#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eleflex.Logging.Message;

namespace Eleflex.Logging.Web.Controllers
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
        public List<Log> Items { get; set; }

    }
}