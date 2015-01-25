using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eleflex.Security.Web.Security.Users
{
    public class ClaimsViewModel
    {

        public ClaimsViewModel()
        {
            UserClaims = new List<UserClaimViewModel>();
        }

        public Guid UserKey { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<UserClaimViewModel> UserClaims { get; set; }
    }
}