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
    public interface IUser : Microsoft.AspNet.Identity.IUser
    {
        System.Guid UserKey { get; set; }
        System.DateTimeOffset CreateDate { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string PasswordSalt { get; set; }
        System.DateTimeOffset PasswordLastChangeDate { get; set; }
        int LoginFailedAttempts { get; set; }
        bool EnableLockout { get; set; }
        Nullable<System.DateTimeOffset> LastLoginDate { get; set; }
        Nullable<System.DateTimeOffset> LockoutReinstateDate { get; set; }
        string Comment { get; set; }                
        bool Inactive { get; set; }
        bool EmailValid { get; set; }
        string EmailValidCode { get; set; }
    }
}
