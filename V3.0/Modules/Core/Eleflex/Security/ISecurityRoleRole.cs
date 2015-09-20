using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityRoleRole.
    /// </summary>
	public partial interface ISecurityRoleRole
	{	

		/// <summary>
		/// The SecurityRoleRoleKey.
		/// </summary>
		long SecurityRoleRoleKey { get; set; }
		/// <summary>
		/// The ParentSecurityRoleKey.
		/// </summary>
		System.Guid ParentSecurityRoleKey { get; set; }
		/// <summary>
		/// The ChildSecurityRoleKey.
		/// </summary>
		System.Guid ChildSecurityRoleKey { get; set; }
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
