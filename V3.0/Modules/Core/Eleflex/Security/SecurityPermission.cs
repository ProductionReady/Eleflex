using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityPermission.
    /// </summary>
	public partial class SecurityPermission : ISecurityPermission
	{	

		/// <summary>
		/// The SecurityPermissionKey.
		/// </summary>
		public virtual System.Guid SecurityPermissionKey { get; set; }
		/// <summary>
		/// The Active.
		/// </summary>
		public virtual bool Active { get; set; }
		/// <summary>
		/// The Name.
		/// </summary>
		public virtual string Name { get; set; }
		/// <summary>
		/// The Description.
		/// </summary>
		public virtual string Description { get; set; }
		/// <summary>
		/// The IsSystem.
		/// </summary>
		public virtual bool IsSystem { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		public virtual string ExtraData { get; set; }

	}
}
