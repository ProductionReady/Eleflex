using System.ServiceModel;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents an object exposing a service command.
    /// </summary>
    [ServiceContract]
    public partial interface IWCFCommand
    {

        /// <summary>
        /// Execute a service command.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [ServiceKnownType("GetKnownTypes", typeof(WCFCommandRegistry))]
        Response ExecuteServiceCommand(Request request);

    }
}
