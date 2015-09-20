using System;
using System.Collections.Generic;

namespace Eleflex.Security.Web.Admin.Models.Users
{
    public class ClaimsViewModel
    {

        public ClaimsViewModel()
        {
            UserClaims = new List<UserClaimViewModel>();
        }

        public Guid SecurityUserKey { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<UserClaimViewModel> UserClaims { get; set; }
    }
}