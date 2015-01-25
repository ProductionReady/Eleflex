using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eleflex.Security.Web.Security.Users
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