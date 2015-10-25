using System;

namespace Eleflex.Email.Services.WCF.Message
{
	/// <summary>
    /// Represents a service model for a EmailProcessAttachment object.
    /// </summary>
	public partial class EmailProcessAttachment
	{	

		/// <summary>
		/// The EmailProcessAttachmentKey.
		/// </summary>
		public virtual long EmailProcessAttachmentKey { get; set; }
		/// <summary>
		/// The EmailProcessKey.
		/// </summary>
		public virtual long EmailProcessKey { get; set; }
		/// <summary>
		/// The Filename.
		/// </summary>
		public virtual string Filename { get; set; }
		/// <summary>
		/// The FileData.
		/// </summary>
		public virtual byte[] FileData { get; set; }
		/// <summary>
		/// The ContentType.
		/// </summary>
		public virtual string ContentType { get; set; }

	}
}
