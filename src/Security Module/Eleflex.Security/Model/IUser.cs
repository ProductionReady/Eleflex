using System;
using System.Collections.Generic;

using Eleflex.Storage;

namespace Eleflex.Security
{
    public interface IUser : IStorageExtraData
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
        bool IsLockedOut { get; set; }
        Nullable<System.DateTimeOffset> LastLoginDate { get; set; }
        Nullable<System.DateTimeOffset> LockoutReinstateDate { get; set; }
        string Comment { get; set; }        
    }
}
