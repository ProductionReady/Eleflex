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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Security
{
    public class UserPermission
    {

        public long UserPermissionKey { get; set; }
        public System.Guid UserKey { get; set; }
        public System.Guid PermissionKey { get; set; }
        public bool Inactive { get; set; }
        public Nullable<System.DateTimeOffset> StartDate { get; set; }
        public Nullable<System.DateTimeOffset> EndDate { get; set; }
        public string Comment { get; set; }
        public string ExtraData { get; set; }

        public void ChangeUserKey(System.Guid val)
        {
            UserKey = val;
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
    }
}
