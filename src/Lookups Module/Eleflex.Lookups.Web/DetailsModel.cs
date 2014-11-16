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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eleflex.Lookups.Message;

namespace Eleflex.Lookups.Web
{
    public class DetailsModel : Lookup
    {


        [Required]
        [MaxLength(50)]
        public override string Abbreviation { get; set; }

        [Required]
        [MaxLength(250)]
        public override string Name { get; set; }

        [MaxLength(2000)]
        public override string Description { get; set; }
        

        /// <summary>
        /// Inactive select items.
        /// </summary>
        public List<SelectListItem> InactiveSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "Active",Value="false"},
                    new SelectListItem(){Text = "Inactive",Value="true"},
                };
            }
        }

    }
}