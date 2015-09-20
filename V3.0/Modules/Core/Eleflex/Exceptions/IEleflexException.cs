using System;
using System.Collections;

namespace Eleflex
{
    /// <summary>
    /// Base interface for all managedexceptions in the application.
    /// </summary>
    public partial interface IEleflexException : IResponse
    {

        /// <summary>
        /// 
        /// Summary:
        ///     Gets a collection of key/value pairs that provide additional user-defined information
        ///     about the exception.
        ///
        /// Returns:
        ///     An object that implements the System.Collections.IDictionary interface and contains
        ///     a collection of user-defined key/value pairs. The default is an empty collection.
        /// </summary>
        IDictionary Data { get; }
        /// <summary>
        /// 
        /// Summary:
        ///     Gets or sets a link to the help file associated with this exception.
        ///
        /// Returns:
        ///     The Uniform Resource Name (URN) or Uniform Resource Locator (URL).
        /// </summary>
        string HelpLink { get; set; }
        /// <summary>
        /// 
        /// Summary:
        ///     Gets or sets HRESULT, a coded numerical value that is assigned to a specific
        ///     exception.
        ///
        /// Returns:
        ///     The HRESULT value.
        /// </summary>
        int HResult { get; }
        /// <summary>
        ///
        /// Summary:
        ///     Gets the System.Exception instance that caused the current exception.
        ///
        /// Returns:
        ///     An object that describes the error that caused the current exception. The System.Exception.InnerException
        ///     property returns the same value as was passed into the System.Exception.#ctor(System.String,System.Exception)
        ///     constructor, or null if the inner exception value was not supplied to the constructor.
        ///     This property is read-only.
        /// </summary>
        Exception InnerException { get; }
        /// <summary>
        ///
        /// Summary:
        ///     Gets a message that describes the current exception.
        ///
        /// Returns:
        ///     The error message that explains the reason for the exception, or an empty string
        ///     ("").
        /// </summary>
        string Message { get; }
        /// <summary>
        ///
        /// Summary:
        ///     Gets or sets the name of the application or the object that causes the error.
        ///
        /// Returns:
        ///     The name of the application or the object that causes the error.
        ///
        /// Exceptions:
        ///   T:System.ArgumentException:
        ///     The object must be a runtime System.Reflection object
        /// </summary>
        string Source { get; set; }
        /// <summary>
        ///
        /// Summary:
        ///     Gets a string representation of the immediate frames on the call stack.
        ///
        /// Returns:
        ///     A string that describes the immediate frames of the call stack.
        /// </summary>
        string StackTrace { get; }

        /// <summary>
        ///
        /// Summary:
        ///     When overridden in a derived class, returns the System.Exception that is the
        ///     root cause of one or more subsequent exceptions.
        ///
        /// Returns:
        ///     The first exception thrown in a chain of exceptions. If the System.Exception.InnerException
        ///     property of the current exception is a null reference (Nothing in Visual Basic),
        ///     this property returns the current exception.
        /// </summary>
        /// <returns></returns>
        Exception GetBaseException();
    }
}
