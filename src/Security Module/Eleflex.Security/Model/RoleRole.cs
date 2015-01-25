using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Security
{
    public class RoleRole
    {

        public long RoleRoleKey { get; set; }
        public System.Guid ParentRoleKey { get; set; }
        public System.Guid ChildRoleKey { get; set; }
        public bool Inactive { get; set; }
        public Nullable<System.DateTimeOffset> StartDate { get; set; }
        public Nullable<System.DateTimeOffset> EndDate { get; set; }
        public string Comment { get; set; }
        public string ExtraData { get; set; }
        public System.Guid? ModuleKey { get; set; }

        public void ChangeParentRoleKey(System.Guid val)
        {
            ParentRoleKey = val;
        }
        public void ChangeChildRoleKey(System.Guid val)
        {
            ChildRoleKey = val;
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
