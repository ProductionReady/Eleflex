using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a SecurityUserLogin.
    /// </summary>
	public partial class SecurityUserLogin : ISecurityUserLogin
	{	

		/// <summary>
		/// The SecurityUserLoginKey.
		/// </summary>
		public virtual long SecurityUserLoginKey { get; set; }
		/// <summary>
		/// The SecurityUserKey.
		/// </summary>
		public virtual System.Guid SecurityUserKey { get; set; }
		/// <summary>
		/// The LoginProvider.
		/// </summary>
		public virtual string LoginProvider { get; set; }
		/// <summary>
		/// The ProviderKey.
		/// </summary>
		public virtual string ProviderKey { get; set; }
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
