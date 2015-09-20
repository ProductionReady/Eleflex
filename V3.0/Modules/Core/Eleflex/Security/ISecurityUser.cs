using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUser.
    /// </summary>
	public partial interface ISecurityUser
	{	

		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		System.Guid SecurityUserKey { get; set; }
		/// <summary>
		/// The Active.
		/// </summary>
		bool Active { get; set; }
		/// <summary>
		/// The CreateDate.
		/// </summary>
		Nullable<System.DateTimeOffset> CreateDate { get; set; }
		/// <summary>
		/// The FirstName.
		/// </summary>
		string FirstName { get; set; }
		/// <summary>
		/// The LastName.
		/// </summary>
		string LastName { get; set; }
		/// <summary>
		/// The Username.
		/// </summary>
		string Username { get; set; }
		/// <summary>
		/// The Email.
		/// </summary>
		string Email { get; set; }
		/// <summary>
		/// The Password.
		/// </summary>
		string Password { get; set; }
		/// <summary>
		/// The PasswordLastChangeDate.
		/// </summary>
		System.DateTimeOffset PasswordLastChangeDate { get; set; }
		/// <summary>
		/// The SecurityStamp.
		/// </summary>
		string SecurityStamp { get; set; }
		/// <summary>
		/// The LoginFailedAttempts.
		/// </summary>
		int LoginFailedAttempts { get; set; }
		/// <summary>
		/// The EnableLockout.
		/// </summary>
		bool EnableLockout { get; set; }
		/// <summary>
		/// The LastLoginDate.
		/// </summary>
		Nullable<System.DateTimeOffset> LastLoginDate { get; set; }
		/// <summary>
		/// The LockoutReinstateDate.
		/// </summary>
		Nullable<System.DateTimeOffset> LockoutReinstateDate { get; set; }
		/// <summary>
		/// The EmailValid.
		/// </summary>
		bool EmailValid { get; set; }
		/// <summary>
		/// The EmailValidCode.
		/// </summary>
		string EmailValidCode { get; set; }
		/// <summary>
		/// The Phone.
		/// </summary>
		string Phone { get; set; }
		/// <summary>
		/// The PhoneValid.
		/// </summary>
		bool PhoneValid { get; set; }
		/// <summary>
		/// The PhoneValidCode.
		/// </summary>
		string PhoneValidCode { get; set; }
		/// <summary>
		/// The TwoFactorAuth.
		/// </summary>
		bool TwoFactorAuth { get; set; }
		/// <summary>
		/// The Comment.
		/// </summary>
		string Comment { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		string ExtraData { get; set; }

	}
}
