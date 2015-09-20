using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUserClaim.
    /// </summary>
	public partial interface ISecurityUserClaim
	{	

		/// <summary>
		/// The SecurityUserClaimKey.
		/// </summary>
		long SecurityUserClaimKey { get; set; }
		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		System.Guid SecurityUserKey { get; set; }
		/// <summary>
		/// The ClaimType.
		/// </summary>
		string ClaimType { get; set; }
		/// <summary>
		/// The ClaimValue.
		/// </summary>
		string ClaimValue { get; set; }
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
