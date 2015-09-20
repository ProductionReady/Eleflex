using System;

namespace Eleflex
{
	/// <summary>
    /// Represents an object for a LogMessage.
    /// </summary>
	public partial class LogMessage : ILogMessage
	{	

		/// <summary>
		/// The LogMessageKey.
		/// </summary>
		public virtual long LogMessageKey { get; set; }
		/// <summary>
		/// The CreateDate.
		/// </summary>
		public virtual System.DateTimeOffset CreateDate { get; set; }
		/// <summary>
		/// The Application.
		/// </summary>
		public virtual string Application { get; set; }
		/// <summary>
		/// The Server.
		/// </summary>
		public virtual string Server { get; set; }
		/// <summary>
		/// The IsError.
		/// </summary>
		public virtual bool IsError { get; set; }
		/// <summary>
		/// The Severity.
		/// </summary>
		public virtual string Severity { get; set; }
		/// <summary>
		/// The Source.
		/// </summary>
		public virtual string Source { get; set; }
		/// <summary>
		/// The Message.
		/// </summary>
		public virtual string Message { get; set; }
		/// <summary>
		/// The Exception.
		/// </summary>
		public virtual string Exception { get; set; }
		/// <summary>
		/// The ExtraData.
		/// </summary>
		public virtual string ExtraData { get; set; }

	}
}
