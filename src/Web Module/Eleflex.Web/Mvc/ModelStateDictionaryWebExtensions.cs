using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Eleflex.Services;

namespace Eleflex.Web
{
    /// <summary>
    /// Extension / helper methods for use with the ModelStateDictionary.
    /// </summary>
    public static class ModelStateDictionaryWebExtensions
    {
        public static bool IsServiceError(this ModelStateDictionary modelStateDictionary, IServiceCommandResponse serviceResponse)
        {
            if (serviceResponse.ResponseStatus.IsError)
            {
                foreach(var message in serviceResponse.ResponseStatus.Messages)
                {
                    if(message.IsError)
                        modelStateDictionary.AddModelError(message.Field, message.Message);
                }
            }
            return serviceResponse.ResponseStatus.IsError;
        }
    }
}