using System.Collections.Generic;
using System.Web.Mvc;
using ServiceModel = Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Web.Admin.Models
{
    /// <summary>
    /// Lookups list model.
    /// </summary>
    public class ListModel
    {

        /// <summary>
        /// Search inactoive
        /// </summary>
        public bool? Active { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// The parent lookup.
        /// </summary>
        public virtual System.Guid? ParentLookupKey { get; set; }

        /// <summary>
        /// Max records to return.
        /// </summary>
        public int? MaxRecords { get; set; }

        /// <summary>
        /// Inactive select items.
        /// </summary>
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

        /// <summary>
        /// Items.
        /// </summary>
        public IList<ServiceModel.Lookup> Items { get; set; }
    }
}