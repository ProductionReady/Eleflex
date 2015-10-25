using System;

namespace Eleflex.Email
{
	/// <summary>
    /// Represents an object for a EmailProcessAttachment.
    /// </summary>
	public partial interface IEmailProcessAttachment
	{	

		/// <summary>
		/// The EmailProcessAttachmentKey.
		/// </summary>
		long EmailProcessAttachmentKey { get; set; }
		/// <summary>
		/// The EmailProcessKey.
		/// </summary>
		long EmailProcessKey { get; set; }
		/// <summary>
		/// The Filename.
		/// </summary>
		string Filename { get; set; }
		/// <summary>
		/// The FileData.
		/// </summary>
		byte[] FileData { get; set; }
		/// <summary>
		/// The ContentType.
		/// </summary>
		string ContentType { get; set; }

	}
}
