using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityRole.
    /// </summary>
	public partial class SecurityRole : ISecurityRole
	{	

		/// <summary>
		/// The SecurityRoleKey.
		/// </summary>
		public virtual System.Guid SecurityRoleKey { get; set; }
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
		/// The EffectiveStartDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> EffectiveStartDate { get; set; }
		/// <summary>
		/// The EffectiveEndDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> EffectiveEndDate { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		public virtual string ExtraData { get; set; }

	}
}
