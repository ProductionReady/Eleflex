using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object used to respond to a request. Generally, methods with Request and Response objects do not throw exceptions.
    /// </summary>
    public partial class Response : IResponse
    {
        /// <summary>
        /// Constructor. By default, all responses are successful unless otherwise set.
        /// </summary>
        public Response()
        {
            ResponseSuccess = true;
            ResponseMessages = new List<IValidationMessage>();
        }


        /// <summary>
        /// Determine if the response is successful or if an error happened.
        /// </summary>
        public virtual bool ResponseSuccess { get; set; }

        /// <summary>
        /// The collection of response messages
        /// </summary>
        public virtual List<IValidationMessage> ResponseMessages { get; set; }

        /// <summary>
        /// Copy the response to this instance.
        /// </summary>
        /// <param name="response"></param>
        public virtual void CopyResponse(IResponse response)
        {
            if (!response.ResponseSuccess)
                this.ResponseSuccess = false;

            if (response.ResponseMessages.Count > 0)
            {
                foreach (var item in response.ResponseMessages)
                {
                    ResponseMessages.Add(item);
                    if (item.IsError)
                        this.ResponseSuccess = false;
                }
            }
        }
        
        /// <summary>
        /// Add a message to the response.
        /// </summary>
        /// <param name="isError"></param>
        /// <param name="messageCode"></param>
        /// <param name="userMessage"></param>
        public virtual void AddMessage(bool isError, string messageCode, string userMessage)
        {
            if (isError)
                this.ResponseSuccess = false;
            ResponseMessages.Add(new ValidationMessage() { IsError = isError, MessageCode = messageCode, UserMessage = userMessage });
        }
    }
}
