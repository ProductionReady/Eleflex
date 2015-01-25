using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Security
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

        public void ChangeRoleKey(System.Guid val)
        {
            RoleKey = val;
        }
        public void ChangePermissionKey(System.Guid val)
        {
            PermissionKey = val;
        }
        public void ChangeInactive(bool val)
        {
            Inactive = val;
        }
        public void ChangeStartDate(Nullable<System.DateTimeOffset> val)
        {
            StartDate = val;
        }
        public void ChangeEndDate(Nullable<System.DateTimeOffset> val)
        {
            EndDate = val;
        }
        public void ChangeComment(string val)
        {
            Comment = val;
        }
        public void ChangeExtraData(string val)
        {
            ExtraData = val;
        }
        public void ChangeModuleKey(Guid? val)
        {
            ModuleKey = val;
        }
    }
}
