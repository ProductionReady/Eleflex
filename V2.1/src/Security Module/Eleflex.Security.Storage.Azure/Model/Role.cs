//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eleflex.Security.Storage.Azure.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Role
    {
        public Role()
        {
            this.RolePermissions = new HashSet<RolePermission>();
            this.RoleRoles = new HashSet<RoleRole>();
            this.RoleRoles1 = new HashSet<RoleRole>();
            this.UserRoles = new HashSet<UserRole>();
        }
    
        public System.Guid RoleKey { get; set; }
        public bool Inactive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExtraData { get; set; }
        public Nullable<System.Guid> ModuleKey { get; set; }
        public Nullable<System.DateTimeOffset> StartDate { get; set; }
        public Nullable<System.DateTimeOffset> EndDate { get; set; }
    
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<RoleRole> RoleRoles { get; set; }
        public virtual ICollection<RoleRole> RoleRoles1 { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}