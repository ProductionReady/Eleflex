using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Eleflex.Web
{
    public class ApplicationRoleManager : RoleManager<Eleflex.Security.Role>
    {

        public ApplicationRoleManager(IRoleStore<Eleflex.Security.Role> store)
            : base(store)
        {
        }

    }
}
