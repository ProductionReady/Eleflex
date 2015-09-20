using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// The base exception for all managed exceptions in the application.
    /// </summary>
    public partial class EleflexException : Exception, IEleflexException
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexException() : base()
        {
            ResponseSuccess = false;
            ResponseMessages = new List<IValidationMessage>();
            ResponseMessages.Add(new ValidationMessage() { MessageCode = MessageConstants.ERROR_SYSTEM_CODE, UserMessage = MessageConstants.ERROR_SYSTEM_TEXT, IsError = true });
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageCode"></param>
        public EleflexException(string messageCode) : base(messageCode)
        {
            ResponseSuccess = false;
            ResponseMessages = new List<IValidationMessage>();
            ResponseMessages.Add(new ValidationMessage() { MessageCode = messageCode, IsError = true });
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="messageText"></param>
        public EleflexException(string messageCode, string messageText) : base(messageCode)
        {
            ResponseSuccess = false;
            ResponseMessages = new List<IValidationMessage>();
            ResponseMessages.Add(new ValidationMessage() { MessageCode = messageCode, UserMessage = messageText, IsError = true });
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messageCode"></param>
        /// <param name="innerException"></param>
        public EleflexException(string messageCode, Exception innerException) : base(messageCode, innerException)
        {
            ResponseSuccess = false;
            ResponseMessages = new List<IValidationMessage>();
            ResponseMessages.Add(new ValidationMessage() { MessageCode = messageCode, IsError = true });
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messages"></param>
        public EleflexException(List<ValidationMessage> messages)
            : base()
        {
            ResponseSuccess = false;
            ResponseMessages = new List<IValidationMessage>();
            foreach (var item in messages)
                ResponseMessages.Add(item);

        }

        /// <summary>
        /// Determine if the response is successful or if an error happened.
        /// </summary>
        public virtual bool ResponseSuccess { get; set; }

        /// <summary>
        /// List of validation messages.
        /// </summary>
        public virtual List<IValidationMessage> ResponseMessages { get; set; }

        /// <summary>
        /// Copy the response to the current instance.
        /// </summary>
        /// <param name="response"></param>
        public virtual void CopyResponse(IResponse response)
        {
            if (!response.ResponseSuccess)
                ResponseSuccess = false;
            foreach (var item in response.ResponseMessages)
                this.ResponseMessages.Add(item);
        }

        /// <summary>
        /// Add a message to the respose.
        /// </summary>
        /// <param name="isError"></param>
        /// <param name="messageCode"></param>
        /// <param name="userMessage"></param>
        public virtual void AddMessage(bool isError, string messageCode, string userMessage)
        {
            if (isError)
                ResponseSuccess = false;
            ResponseMessages.Add(new ValidationMessage() { IsError = isError, MessageCode = messageCode, UserMessage = userMessage });
        }
    }
}
