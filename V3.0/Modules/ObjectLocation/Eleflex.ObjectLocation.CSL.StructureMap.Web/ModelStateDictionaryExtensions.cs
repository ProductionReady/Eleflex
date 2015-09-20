using System.Web.Mvc;

namespace Eleflex.ObjectLocation.CSL.StructureMap.Web
{
    /// <summary>
    /// Extension / helper methods for use with the ModelStateDictionary.
    /// </summary>
    public static partial class ModelStateDictionaryExtensions
    {

        /// <summary>
        /// Determine if the response is an error and set model errors automatically.
        /// </summary>
        /// <param name="modelStateDictionary"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool IsError(this ModelStateDictionary modelStateDictionary, IResponse response)
        {            
            if (response == null)
            {
                modelStateDictionary.AddModelError(string.Empty, "Response null");
                return true;
            }
            if (!response.ResponseSuccess)
            {
                if (response.ResponseMessages != null && response.ResponseMessages.Count > 0)
                {
                    foreach (var message in response.ResponseMessages)
                    {
                        if (message.IsError)
                        {
                            if(message.Fields.Count > 0)
                            {
                                foreach (string field in message.Fields)
                                    modelStateDictionary.AddModelError(field, message.UserMessage);
                            }
                            else
                                modelStateDictionary.AddModelError(string.Empty, message.UserMessage);
                        }                        
                    }
                }
                else
                    modelStateDictionary.AddModelError(string.Empty, MessageConstants.ERROR_SYSTEM_TEXT);                
            }            
            return !response.ResponseSuccess;
        }
    }
}