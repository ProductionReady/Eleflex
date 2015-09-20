using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUserRole.
    /// </summary>
	public partial class SecurityUserRole : ISecurityUserRole
	{	

		/// <summary>
		/// The SecurityUserRoleKey.
		/// </summary>
		public virtual long SecurityUserRoleKey { get; set; }
		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		public virtual System.Guid SecurityUserKey { get; set; }
		/// <summary>
		/// The SecurityRoleKey.
		/// </summary>
		public virtual System.Guid SecurityRoleKey { get; set; }
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
