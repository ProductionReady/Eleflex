using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Security.Message
{
    public class RolePermission
    {

        public long RolePermissionKey { get; set; }
        public System.Guid RoleKey { get; set; }
        public System.Guid PermissionKey { get; set; }
        public bool Inactive { get; set; }
        public Nullable<System.DateTimeOffset> StartDate { get; set; }
        public Nullable<System.DateTimeOffset> EndDate { get; set; }
        public string Comment { get; set; }
        public string ExtraData { get; set; }
        public System.Guid? ModuleKey { get; set; }

    }
}
