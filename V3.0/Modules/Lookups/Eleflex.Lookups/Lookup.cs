using System;

namespace Eleflex.Lookups
{
	/// <summary>
    /// Represents an object for a Lookup.
    /// </summary>
	public partial class Lookup : ILookup
	{	

		/// <summary>
		/// The LookupKey.
		/// </summary>
		public virtual System.Guid LookupKey { get; set; }
		/// <summary>
		/// The ParentLookupKey.
		/// </summary>
		public virtual Nullable<System.Guid> ParentLookupKey { get; set; }
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
		/// The SortOrder.
		/// </summary>
		public virtual Nullable<int> SortOrder { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		public virtual string ExtraData { get; set; }

	}
}
