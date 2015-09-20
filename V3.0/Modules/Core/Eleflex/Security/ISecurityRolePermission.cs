using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityRolePermission.
    /// </summary>
	public partial interface ISecurityRolePermission
	{	

		/// <summary>
		/// The SecurityRolePermissionKey.
		/// </summary>
		long SecurityRolePermissionKey { get; set; }
		/// <summary>
		/// The SecurityRoleKey.
		/// </summary>
		System.Guid SecurityRoleKey { get; set; }
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
		/// The IsSystem.
		/// </summary>
		bool IsSystem { get; set; }
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
