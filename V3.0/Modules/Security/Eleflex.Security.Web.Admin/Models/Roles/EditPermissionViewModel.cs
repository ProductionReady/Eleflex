using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Eleflex.Security.Web.Admin.Models.Roles
{
    public class EditPermissionViewModel : IValidatableObject
    {

        public EditPermissionViewModel()
        {
            Active = true;
        }

        public Guid SecurityRoleKey { get; set; }
        public string SearchName { get; set; }
        

        public long? SecurityRolePermissionKey { get; set; }
        public System.Guid? SecurityPermissionKey { get; set; }
        
        public string SelectedPermission { get; set; }

        [MaxLength(2000)]
        public string Comment { get; set; }

        public bool Active { get; set; }
        public string ExtraData { get; set; }
        public Guid? ModuleKey { get; set; }
        public Nullable<System.DateTimeOffset> EffectiveStartDate { get; set; }
        public Nullable<System.DateTimeOffset> EffectiveEndDate { get; set; }
        public Nullable<System.DateTime> StartDateLocalTime
        {
            get
            {
                if (EffectiveStartDate.HasValue)
                    return EffectiveStartDate.Value.ToLocalTime().DateTime;
                return null;
            }
            set
            {
                DateTimeOffset? val = value;
                if (val.HasValue)
                    EffectiveStartDate = val.Value.ToUniversalTime();
                else
                    EffectiveStartDate = null;
            }
        }
        public Nullable<System.DateTime> EndDateLocalTime
        {
            get
            {
                if (EffectiveEndDate.HasValue)
                    return EffectiveEndDate.Value.ToLocalTime().DateTime;
                return null;
            }
            set
            {
                DateTimeOffset? val = value;
                if (val.HasValue)
                    EffectiveEndDate = val.Value.ToUniversalTime();
                else
                    EffectiveEndDate = null;
            }
        }
        public string SuccessMessage { get; set; }

        public bool IsSystemRole
        {
            get { return ModuleKey.HasValue; }
        }
        /// <summary>
        /// Inactive select items.
        /// </summary>
        public List<SelectListItem> ActiveSelectItems
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "Active",Value="true"},
                    new SelectListItem(){Text = "Inactive",Value="false"},
                };
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> result = new List<ValidationResult>();
            if (EffectiveEndDate.HasValue && EffectiveStartDate.HasValue && EffectiveEndDate < EffectiveStartDate)
                result.Add(new ValidationResult("EndDate must be greater than StartDate"));

            return result;
        }
    }
}