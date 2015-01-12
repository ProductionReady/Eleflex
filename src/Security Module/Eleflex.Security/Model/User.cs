using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Eleflex.Storage;
using System.Security.Claims;

namespace Eleflex.Security
{
    /// <summary>
    /// Defines a user in the system.
    /// </summary>
    public partial class User : Eleflex.Security.IUser, IStorageExtraData
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
        public bool EnableLockout { get; set; }
        public Nullable<System.DateTimeOffset> LastLoginDate { get; set; }
        public Nullable<System.DateTimeOffset> LockoutReinstateDate { get; set; }
        public string Comment { get; set; }        
        public string ExtraData { get; set; }
        public bool Inactive { get; set; }
        public bool EmailValid { get; set; }
        public string EmailValidCode { get; set; }
        public string Phone { get; set; }
        public bool PhoneValid { get; set; }
        public string PhoneValidCode { get; set; }
        public bool TwoFactorAuth { get; set; }



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
        public void ChangeEnableLockout(bool val)
        {
            EnableLockout = val;
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
        public void ChangeInactive(bool val)
        {
            Inactive = val;
        }
        public void ChangeEmailValid(bool val)
        {
            EmailValid = val;
        }
        public void ChangeEmailValidCode(string val)
        {
            EmailValidCode = val;
        }
        public void ChangePhone(string val)
        {
            Phone = val;
        }
        public void ChangePhoneValid(bool val)
        {
            PhoneValid = val;
        }
        public void ChangePhoneValidCode(string val)
        {
            PhoneValidCode = val;
        }
        public void ChangeTwoFactorAuth(bool val)
        {
            TwoFactorAuth = val;
        }
        //Microsoft ASP.Net Identity integration

        public string Id
        {
            get { return UserKey.ToString(); }
        }

        public string UserName
        {
            get
            {
                return Username;
            }
            set
            {
                ChangeUsername(value);
            }
        }
    }
}
