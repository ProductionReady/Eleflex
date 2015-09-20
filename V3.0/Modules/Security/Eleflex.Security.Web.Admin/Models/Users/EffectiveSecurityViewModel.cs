using System.Collections.Generic;

namespace Eleflex.Security.Web.Admin.Models.Users
{
    public class EffectiveSecurityViewModel
    {

        public EffectiveSecurityViewModel()
        {
            Roles = new List<string>();
        }
        public List<string> Roles { get; set; }

        
    }
}