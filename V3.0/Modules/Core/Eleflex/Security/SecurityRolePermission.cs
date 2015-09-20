using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityRolePermission.
    /// </summary>
	public partial class SecurityRolePermission : ISecurityRolePermission
	{	

		/// <summary>
		/// The SecurityRolePermissionKey.
		/// </summary>
		public virtual long SecurityRolePermissionKey { get; set; }
		/// <summary>
		/// The SecurityRoleKey.
		/// </summary>
		public virtual System.Guid SecurityRoleKey { get; set; }
		/// <summary>
		/// The SecurityPermissionKey.
		/// </summary>
		public virtual System.Guid SecurityPermissionKey { get; set; }
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
		/// The IsSystem.
		/// </summary>
		public virtual bool IsSystem { get; set; }
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
