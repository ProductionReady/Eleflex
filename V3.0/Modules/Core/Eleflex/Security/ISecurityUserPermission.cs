using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUserPermission.
    /// </summary>
	public partial interface ISecurityUserPermission
	{	

		/// <summary>
		/// The SecurityUserPermissionKey.
		/// </summary>
		long SecurityUserPermissionKey { get; set; }
		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		System.Guid SecurityUserKey { get; set; }
		/// <summary>
		/// The SecurityPermissionKey.
		/// </summary>
		System.Guid SecurityPermissionKey { get; set; }
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
