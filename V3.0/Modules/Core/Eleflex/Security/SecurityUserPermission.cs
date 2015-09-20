using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUserPermission.
    /// </summary>
	public partial class SecurityUserPermission : ISecurityUserPermission
	{	

		/// <summary>
		/// The SecurityUserPermissionKey.
		/// </summary>
		public virtual long SecurityUserPermissionKey { get; set; }
		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		public virtual System.Guid SecurityUserKey { get; set; }
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
		/// The Comment.
		/// </summary>
		public virtual string Comment { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		public virtual string ExtraData { get; set; }

	}
}
