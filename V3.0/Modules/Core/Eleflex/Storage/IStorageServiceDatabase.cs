namespace Eleflex
{
    /// <summary>
    /// Interface for a database storage service.
    /// </summary>
    public partial interface IStorageServiceDatabase : IStorageService
    {

        /// <summary>
        /// Get the simple connection string.
        /// </summary>
        string SimpleConnectionString { get; }

        /// <summary>
        /// Get the connection string tailored to provider's requirements.
        /// </summary>
        string ProviderConnectionString { get; }
        

    }
}
