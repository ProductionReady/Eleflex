//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eleflex.Versioning.Storage.SqlServer.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModuleVersion
    {
        public System.Guid ModuleKey { get; set; }
        public string ModuleName { get; set; }
        public System.DateTimeOffset UpdateDate { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }
        public string ExtraData { get; set; }
    }
}