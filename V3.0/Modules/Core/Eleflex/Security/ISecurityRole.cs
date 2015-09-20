using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityRole.
    /// </summary>
	public partial interface ISecurityRole
	{	

		/// <summary>
		/// The SecurityRoleKey.
		/// </summary>
		System.Guid SecurityRoleKey { get; set; }
		/// <summary>
		/// The Active.
		/// </summary>
		bool Active { get; set; }
		/// <summary>
		/// The Name.
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// The Description.
		/// </summary>
		string Description { get; set; }
		/// <summary>
		/// The IsSystem.
		/// </summary>
		bool IsSystem { get; set; }
		/// <summary>
		/// The EffectiveStartDate.
		/// </summary>
		Nullable<System.DateTimeOffset> EffectiveStartDate { get; set; }
		/// <summary>
		/// The EffectiveEndDate.
		/// </summary>
		Nullable<System.DateTimeOffset> EffectiveEndDate { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		string ExtraData { get; set; }

	}
}
