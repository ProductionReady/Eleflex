using System;

namespace Eleflex.Email
{
	/// <summary>
    /// Represents an object for a EmailProcess.
    /// </summary>
	public partial class EmailProcess : IEmailProcess
	{	

		/// <summary>
		/// The EmailProcessKey.
		/// </summary>
		public virtual long EmailProcessKey { get; set; }
		/// <summary>
		/// The FromAddress.
		/// </summary>
		public virtual string FromAddress { get; set; }
		/// <summary>
		/// The ToAddress.
		/// </summary>
		public virtual string ToAddress { get; set; }
		/// <summary>
		/// The CcAddress.
		/// </summary>
		public virtual string CcAddress { get; set; }
		/// <summary>
		/// The BccAddress.
		/// </summary>
		public virtual string BccAddress { get; set; }
		/// <summary>
		/// The Subject.
		/// </summary>
		public virtual string Subject { get; set; }
		/// <summary>
		/// The Body.
		/// </summary>
		public virtual string Body { get; set; }
		/// <summary>
		/// The IsHtml.
		/// </summary>
		public virtual bool IsHtml { get; set; }
		/// <summary>
		/// The CreateDate.
		/// </summary>
		public virtual System.DateTimeOffset CreateDate { get; set; }
		/// <summary>
		/// The FutureSendDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> FutureSendDate { get; set; }
		/// <summary>
		/// The SentDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> SentDate { get; set; }
		/// <summary>
		/// The IsError.
		/// </summary>
		public virtual bool IsError { get; set; }
		/// <summary>
		/// The ErrorDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> ErrorDate { get; set; }
		/// <summary>
		/// The ErrorMessage.
		/// </summary>
		public virtual string ErrorMessage { get; set; }
		/// <summary>
		/// The IsProcessing.
		/// </summary>
		public virtual bool IsProcessing { get; set; }
		/// <summary>
		/// The ProcessingDate.
		/// </summary>
		public virtual Nullable<System.DateTimeOffset> ProcessingDate { get; set; }

	}
}
