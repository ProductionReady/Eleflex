//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eleflex.Lookups.Storage.SqlServer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Lookup
    {
        public Lookup()
        {
            this.Child = new HashSet<Lookup>();
        }
    
        public System.Guid LookupKey { get; set; }
        public string Name { get; set; }
        public Nullable<System.Guid> CategoryKey { get; set; }
        public bool Inactive { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public string Abbreviation { get; set; }
        public string Description { get; set; }
        public string ExtraData { get; set; }
    
        public virtual ICollection<Lookup> Child { get; set; }
        public virtual Lookup Parent { get; set; }
    }
}
