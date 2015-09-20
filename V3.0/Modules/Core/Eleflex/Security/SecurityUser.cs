using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUser.
    /// </summary>
	public partial class SecurityUser : ISecurityUser
	{	

		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		public virtual System.Guid SecurityUserKey { get; set; }
		/// <summary>
		/// The Active.
		/// </summary>
		public virtual bool Active { get; set; }
		/// <summary>
		/// The CreateDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> CreateDate { get; set; }
		/// <summary>
		/// The FirstName.
		/// </summary>
		public virtual string FirstName { get; set; }
		/// <summary>
		/// The LastName.
		/// </summary>
		public virtual string LastName { get; set; }
		/// <summary>
		/// The Username.
		/// </summary>
		public virtual string Username { get; set; }
		/// <summary>
		/// The Email.
		/// </summary>
		public virtual string Email { get; set; }
		/// <summary>
		/// The Password.
		/// </summary>
		public virtual string Password { get; set; }
		/// <summary>
		/// The PasswordLastChangeDate.
		/// </summary>
		public virtual System.DateTimeOffset PasswordLastChangeDate { get; set; }
		/// <summary>
		/// The SecurityStamp.
		/// </summary>
		public virtual string SecurityStamp { get; set; }
		/// <summary>
		/// The LoginFailedAttempts.
		/// </summary>
		public virtual int LoginFailedAttempts { get; set; }
		/// <summary>
		/// The EnableLockout.
		/// </summary>
		public virtual bool EnableLockout { get; set; }
		/// <summary>
		/// The LastLoginDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> LastLoginDate { get; set; }
		/// <summary>
		/// The LockoutReinstateDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> LockoutReinstateDate { get; set; }
		/// <summary>
		/// The EmailValid.
		/// </summary>
		public virtual bool EmailValid { get; set; }
		/// <summary>
		/// The EmailValidCode.
		/// </summary>
		public virtual string EmailValidCode { get; set; }
		/// <summary>
		/// The Phone.
		/// </summary>
		public virtual string Phone { get; set; }
		/// <summary>
		/// The PhoneValid.
		/// </summary>
		public virtual bool PhoneValid { get; set; }
		/// <summary>
		/// The PhoneValidCode.
		/// </summary>
		public virtual string PhoneValidCode { get; set; }
		/// <summary>
		/// The TwoFactorAuth.
		/// </summary>
		public virtual bool TwoFactorAuth { get; set; }
		/// <summary>
		/// The Comment.
		/// </summary>
		public virtual string Comment { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		public virtual string ExtraData { get; set; }

	}
}
