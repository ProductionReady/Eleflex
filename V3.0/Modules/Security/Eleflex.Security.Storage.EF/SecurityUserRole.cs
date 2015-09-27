//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eleflex.Security.Storage.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class SecurityUserRole
    {
        public long SecurityUserRoleKey { get; set; }
        public System.Guid SecurityUserKey { get; set; }
        public System.Guid SecurityRoleKey { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTimeOffset> EffectiveStartDate { get; set; }
        public Nullable<System.DateTimeOffset> EffectiveEndDate { get; set; }
        public string Comment { get; set; }
        public string ExtraData { get; set; }
    
        public virtual SecurityRole SecurityRole { get; set; }
        public virtual SecurityUser SecurityUser { get; set; }
    }
}