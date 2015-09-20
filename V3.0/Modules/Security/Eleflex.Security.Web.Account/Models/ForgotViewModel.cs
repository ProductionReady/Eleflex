using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eleflex.Security.Web.Account.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}