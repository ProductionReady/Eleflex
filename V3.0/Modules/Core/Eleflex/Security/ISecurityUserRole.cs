using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUserRole.
    /// </summary>
	public partial interface ISecurityUserRole
	{	

		/// <summary>
		/// The SecurityUserRoleKey.
		/// </summary>
		long SecurityUserRoleKey { get; set; }
		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		System.Guid SecurityUserKey { get; set; }
		/// <summary>
		/// The SecurityRoleKey.
		/// </summary>
		System.Guid SecurityRoleKey { get; set; }
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
