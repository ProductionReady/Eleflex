using System;
using System.Collections.Generic;
using Eleflex.Storage;

namespace Eleflex.Security
{
    public partial class Role : IStorageExtraData
    {
        public System.Guid RoleKey { get; set; }
        public bool Inactive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExtraData { get; set; }

        public void ChangeRoleKey (System.Guid val)
        {
            RoleKey = val;
        }
        
        public void ChangeInactive (bool val)
        {
            Inactive = val;
        }

        public void ChangeName (string val)
        {
            Name = val;
        }

        public void ChangeDescription (string val)
        {
            Description = val;
        }

        public void ChangeExtraData (string val)
        {
            ExtraData = val;
        }
        
    }
}
