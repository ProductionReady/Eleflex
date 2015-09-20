using System;

namespace Eleflex.Lookups
{
	/// <summary>
    /// Represents an object for a Lookup.
    /// </summary>
	public partial interface ILookup
	{	

		/// <summary>
		/// The LookupKey.
		/// </summary>
		System.Guid LookupKey { get; set; }
		/// <summary>
		/// The ParentLookupKey.
		/// </summary>
		Nullable<System.Guid> ParentLookupKey { get; set; }
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
		/// The SortOrder.
		/// </summary>
		Nullable<int> SortOrder { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		string ExtraData { get; set; }

	}
}
