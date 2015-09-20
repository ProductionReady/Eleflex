using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object used to respond to a request. Generally, methods with Request and Response objects do not throw exceptions.
    /// </summary>
    public partial interface IResponse
    {
        /// <summary>
        /// Determine if the response is successful or if an error happened.
        /// </summary>
        bool ResponseSuccess { get; set; }

        /// <summary>
        /// The collection of response messages.
        /// </summary>
        List<IValidationMessage> ResponseMessages { get; set; }

        /// <summary>
        /// Copy the response to this instance.
        /// </summary>
        /// <param name="response"></param>
        void CopyResponse(IResponse response);

        /// <summary>
        /// Add a message to the response.
        /// </summary>
        /// <param name="isError"></param>
        /// <param name="messageCode"></param>
        /// <param name="userMessage"></param>
        void AddMessage(bool isError, string messageCode, string userMessage);
    }
}
