using ServiceModel = Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.Message
{
    /// <summary>
    /// Query response.
    /// </summary>
    public partial class LookupQueryResponse : StorageQueryResponseItems<ServiceModel.Lookup>
    {
    }
}
