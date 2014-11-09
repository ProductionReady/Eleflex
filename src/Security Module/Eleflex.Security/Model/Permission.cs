using System;
using System.Collections.Generic;
using Eleflex.Storage;

namespace Eleflex.Security 
{
    /// <summary>
    /// Defines a permission in the application.
    /// </summary>
    public partial class Permission : IStorageExtraData
    {
        public System.Guid PermissionKey { get; set; }
        public Nullable<System.Guid> ModuleKey { get; set; }
        public bool Inactive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExtraData { get; set; }

        public void ChangePermissionKey (System.Guid val)
        {
            PermissionKey = val;
        }
        
        public void ChangeModuleKey (Nullable<System.Guid> val)
        {
            ModuleKey = val;
        }
        
        public void ChangeInactive (bool val)
        {
            Inactive = val;
        }
        
        public void ChangeName (string val)
        {
            Name = val;
        }

        public void ChangeDescription (string  val)
        {
            Description = val;
        }
        
        public void ChangeExtraData (string val)
        {
            ExtraData = val;
        }
    }
}
