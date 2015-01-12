using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eleflex.Security.Web.Account
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}