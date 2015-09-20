﻿using System.Collections.Generic;
using System.Web.Mvc;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Web.Admin.Models.Permissions
{
    /// <summary>
    /// Permissions list model.
    /// </summary>
    public class ListViewModel
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string Active { get; set; }

        public int? MaxRecords { get; set; }

        public List<SelectListItem> ActiveSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "",Value=""},
                    new SelectListItem(){Text = "Active",Value="true"},
                    new SelectListItem(){Text = "Inactive",Value="false"},
                };
            }
        }

        public IList<ServiceModel.SecurityPermission> Items { get; set; }
    }
}