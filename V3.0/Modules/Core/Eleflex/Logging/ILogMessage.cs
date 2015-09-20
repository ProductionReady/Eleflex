using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a LogMessage.
    /// </summary>
	public partial interface ILogMessage
	{	

		/// <summary>
		/// The LogMessageKey.
		/// </summary>
		long LogMessageKey { get; set; }
		/// <summary>
		/// The CreateDate.
		/// </summary>
		System.DateTimeOffset CreateDate { get; set; }
		/// <summary>
		/// The Application.
		/// </summary>
		string Application { get; set; }
		/// <summary>
		/// The Server.
		/// </summary>
		string Server { get; set; }
		/// <summary>
		/// The IsError.
		/// </summary>
		bool IsError { get; set; }
		/// <summary>
		/// The Severity.
		/// </summary>
		string Severity { get; set; }
		/// <summary>
		/// The Source.
		/// </summary>
		string Source { get; set; }
		/// <summary>
		/// The Message.
		/// </summary>
		string Message { get; set; }
		/// <summary>
		/// The Exception.
		/// </summary>
		string Exception { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		string ExtraData { get; set; }

	}
}
