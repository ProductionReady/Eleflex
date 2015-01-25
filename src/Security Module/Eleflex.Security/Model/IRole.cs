using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Security
{
    public interface IRole : Microsoft.AspNet.Identity.IRole
    {
        System.Guid RoleKey { get; set; }

        string Description { get; set; }

    }
}
