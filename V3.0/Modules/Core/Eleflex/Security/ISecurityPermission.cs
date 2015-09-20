using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityPermission.
    /// </summary>
	public partial interface ISecurityPermission
	{	

		/// <summary>
		/// The SecurityPermissionKey.
		/// </summary>
		System.Guid SecurityPermissionKey { get; set; }
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
		/// The ExtraData.
		/// </summary>
		string ExtraData { get; set; }

	}
}
