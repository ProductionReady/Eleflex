using System;
using System.Collections.Generic;
using Eleflex.Storage;

namespace Eleflex.Security
{
    /// <summary>
    /// Defines a user in the system.
    /// </summary>
    public partial class User : IUser
    {
        public System.Guid UserKey { get; set; }
        public System.DateTimeOffset CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public System.DateTimeOffset PasswordLastChangeDate { get; set; }
        public int LoginFailedAttempts { get; set; }
        public bool IsLockedOut { get; set; }
        public Nullable<System.DateTimeOffset> LastLoginDate { get; set; }
        public Nullable<System.DateTimeOffset> LockoutReinstateDate { get; set; }
        public string Comment { get; set; }        
        public string ExtraData { get; set; }

        public void ChangeUserKey (System.Guid val)
        {
            UserKey = val;
        }
        public void ChangeCreateDate(DateTimeOffset val)
        {
            CreateDate = val;
        }

        public void ChangeFirstName(string val)
        {
            FirstName = val;
        }
        public void ChangeLastName (string val)
        {
            LastName = val;
        }
        public void ChangeUsername (string val)
        {
            Username = val;
        }
        public void ChangeEmail (string val)
        {
            Email = val;
        }
        public void ChangePassword (string val)
        {
            Password = val;
        }
        public void ChangePasswordSalt (string val)
        {
            PasswordSalt = val;
        }
        public void ChangePasswordLastChangeDate (System.DateTimeOffset val)
        {
            PasswordLastChangeDate = val;
        }
        public void ChangeLoginFailedAttempts (int val)
        {
            LoginFailedAttempts = val;
        }
        public void ChangeIsLockedOut (bool val)
        {
            IsLockedOut = val;
        }
        public void ChangeLastLoginDate (Nullable<System.DateTimeOffset> val)
        {
            LastLoginDate = val;
        }
        public void ChangeLockoutReinstateDate (Nullable<System.DateTimeOffset> val)
        {
            LockoutReinstateDate = val;
        }
        public void ChangeComment (string val)
        {
            Comment = val;
        }
        public void ChangeExtraData (string val)
        {
            ExtraData = val;
        }
    }
}
