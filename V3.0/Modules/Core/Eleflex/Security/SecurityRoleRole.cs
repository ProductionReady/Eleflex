using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityRoleRole.
    /// </summary>
	public partial class SecurityRoleRole : ISecurityRoleRole
	{	

		/// <summary>
		/// The SecurityRoleRoleKey.
		/// </summary>
		public virtual long SecurityRoleRoleKey { get; set; }
		/// <summary>
		/// The ParentSecurityRoleKey.
		/// </summary>
		public virtual System.Guid ParentSecurityRoleKey { get; set; }
		/// <summary>
		/// The ChildSecurityRoleKey.
		/// </summary>
		public virtual System.Guid ChildSecurityRoleKey { get; set; }
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
