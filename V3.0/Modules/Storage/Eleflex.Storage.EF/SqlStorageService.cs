namespace Eleflex.Storage.EF
{
    /// <summary>
    /// Provides storage service base class for generic sql connection and utility functions.
    /// </summary>
    public abstract partial class SqlStorageService : StorageServiceDatabase
    {
        private readonly IStorageContextUnitOfWork _storageContextUnitOfWork;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="serviceKey"></param>
        /// <param name="connectionStringKey"></param>
        /// <param name="versioningStorageConfig"></param>
        /// <param name="storageContextUnitOfWork"></param>
        public SqlStorageService(string serviceKey, string connectionStringKey, string versioningStorageConfig, IStorageContextUnitOfWork storageContextUnitOfWork)
            : base()
        {
            _storageContextUnitOfWork = storageContextUnitOfWork;
            IStorageService service = storageContextUnitOfWork.GetStorageService(serviceKey);
            if (service == null)
            {
                _storageContextUnitOfWork.RegisterStorageService(this);
                SessionOwner = true;
            }
            else
            {
                this.Sessions = service.Sessions; //Get the session list from UOW master provider.
                SessionOwner = false;
            }
            VersioningStorageConfig = versioningStorageConfig;
        }

        /// <summary>
        /// Get the connection string from the app/web.config file
        /// </summary>
        public override string ProviderConnectionString
        {
            get
            {
                return SimpleConnectionString;
            }
        }

    }
}
