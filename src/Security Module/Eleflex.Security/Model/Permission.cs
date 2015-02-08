#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
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
