using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUserClaim.
    /// </summary>
	public partial class SecurityUserClaim : ISecurityUserClaim
	{	

		/// <summary>
		/// The SecurityUserClaimKey.
		/// </summary>
		public virtual long SecurityUserClaimKey { get; set; }
		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		public virtual System.Guid SecurityUserKey { get; set; }
		/// <summary>
		/// The ClaimType.
		/// </summary>
		public virtual string ClaimType { get; set; }
		/// <summary>
		/// The ClaimValue.
		/// </summary>
		public virtual string ClaimValue { get; set; }
		/// <summary>
		/// The Active.
		/// </summary>
		public virtual bool Active { get; set; }
		/// <summary>
		/// The EffectiveStartDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> EffectiveStartDate { get; set; }
		/// <summary>
		/// The EffectiveEndDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> EffectiveEndDate { get; set; }
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
