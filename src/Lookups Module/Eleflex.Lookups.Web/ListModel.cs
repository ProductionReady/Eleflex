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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eleflex.Lookups.Message;

namespace Eleflex.Lookups.Web
{
    /// <summary>
    /// Lookups list model.
    /// </summary>
    public class ListModel
    {

        /// <summary>
        /// Search inactoive
        /// </summary>
        public bool? Inactive { get; set; }

        /// <summary>
        /// The abbreviation.
        /// </summary>
        public virtual string Abbreviation { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Max records to return.
        /// </summary>
        public int? MaxRecords { get; set; }

        /// <summary>
        /// Inactive select items.
        /// </summary>
        public List<SelectListItem> InactiveSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "",Value=""},
                    new SelectListItem(){Text = "Active",Value="false"},
                    new SelectListItem(){Text = "Inactive",Value="true"},
                };
            }
        }

        /// <summary>
        /// Items.
        /// </summary>
        public List<Lookup> Items { get; set; }
    }
}