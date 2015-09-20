using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUserLogin.
    /// </summary>
	public partial interface ISecurityUserLogin
	{	

		/// <summary>
		/// The SecurityUserLoginKey.
		/// </summary>
		long SecurityUserLoginKey { get; set; }
		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		System.Guid SecurityUserKey { get; set; }
		/// <summary>
		/// The LoginProvider.
		/// </summary>
		string LoginProvider { get; set; }
		/// <summary>
		/// The ProviderKey.
		/// </summary>
		string ProviderKey { get; set; }
		/// <summary>
		/// The Active.
		/// </summary>
		bool Active { get; set; }
		/// <summary>
		/// The EffectiveStartDate.
		/// </summary>
		Nullable<System.DateTimeOffset> EffectiveStartDate { get; set; }
		/// <summary>
		/// The EffectiveEndDate.
		/// </summary>
		Nullable<System.DateTimeOffset> EffectiveEndDate { get; set; }
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
