using System;

namespace Eleflex.Email
{
	/// <summary>
    /// Represents an object for a EmailProcess.
    /// </summary>
	public partial interface IEmailProcess
	{	

		/// <summary>
		/// The EmailProcessKey.
		/// </summary>
		long EmailProcessKey { get; set; }
		/// <summary>
		/// The FromAddress.
		/// </summary>
		string FromAddress { get; set; }
		/// <summary>
		/// The ToAddress.
		/// </summary>
		string ToAddress { get; set; }
		/// <summary>
		/// The CcAddress.
		/// </summary>
		string CcAddress { get; set; }
		/// <summary>
		/// The BccAddress.
		/// </summary>
		string BccAddress { get; set; }
		/// <summary>
		/// The Subject.
		/// </summary>
		string Subject { get; set; }
		/// <summary>
		/// The Body.
		/// </summary>
		string Body { get; set; }
		/// <summary>
		/// The IsHtml.
		/// </summary>
		bool IsHtml { get; set; }
		/// <summary>
		/// The CreateDate.
		/// </summary>
		System.DateTimeOffset CreateDate { get; set; }
		/// <summary>
		/// The FutureSendDate.
		/// </summary>
		Nullable<System.DateTimeOffset> FutureSendDate { get; set; }
		/// <summary>
		/// The SentDate.
		/// </summary>
		Nullable<System.DateTimeOffset> SentDate { get; set; }
		/// <summary>
		/// The IsError.
		/// </summary>
		bool IsError { get; set; }
		/// <summary>
		/// The ErrorDate.
		/// </summary>
		Nullable<System.DateTimeOffset> ErrorDate { get; set; }
		/// <summary>
		/// The ErrorMessage.
		/// </summary>
		string ErrorMessage { get; set; }
		/// <summary>
		/// The IsProcessing.
		/// </summary>
		bool IsProcessing { get; set; }
		/// <summary>
		/// The ProcessingDate.
		/// </summary>
		Nullable<System.DateTimeOffset> ProcessingDate { get; set; }

	}
}
