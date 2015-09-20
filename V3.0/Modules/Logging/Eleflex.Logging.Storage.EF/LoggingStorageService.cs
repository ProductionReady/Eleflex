using System.Data.Entity;
using Eleflex.Storage.EF;

namespace Eleflex.Logging.Storage.EF
{
    /// <summary>
    /// Provides abstraction to the Logging storage mechanism.
    /// </summary>
    public partial class LoggingStorageService : EntityStorageService, ILoggingStorageService
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionStringKey"></param>
		/// <param name="versioningStorageConfig"></param>
        /// <param name="storageContextUnitOfWork"></param>
        public LoggingStorageService(string connectionStringKey, string versioningStorageConfig, IStorageContextUnitOfWork storageContextUnitOfWork)
            : base(LoggingConstants.STORAGE_SERVICE_NAME, connectionStringKey, versioningStorageConfig, storageContextUnitOfWork)
        {
        }

        /// <summary>
        /// Get the EF module namespace.
        /// </summary>
        public override string GetEFDatabaseName
        {
            get { return "LoggingDB"; }
        }

        /// <summary>
        /// Create a session, base class will take care of management.
        /// </summary>
        /// <returns></returns>
        public override IStorageServiceSession CreateNonManagedSession()
        {
            LoggingDB context = new LoggingDB(ProviderConnectionString);

            DbContextTransaction transaction = context.Database.BeginTransaction();
            EntityStorageSession session = new EntityStorageSession(context, transaction);
            return session;
        }

    }
}
