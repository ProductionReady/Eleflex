using System.Data.Entity;
using Eleflex.Storage.EF;

namespace Eleflex.Versioning.Storage.EF
{
    /// <summary>
    /// Provides abstraction to the Versioning storage mechanism.
    /// </summary>
    public partial class VersioningStorageService : EntityStorageService, IVersioningStorageService
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionStringKey"></param>
		/// <param name="versioningStorageConfig"></param>
        /// <param name="storageContextUnitOfWork"></param>
        public VersioningStorageService(string connectionStringKey, string versioningStorageConfig, IStorageContextUnitOfWork storageContextUnitOfWork)
            : base(VersioningConstants.STORAGE_SERVICE_NAME, connectionStringKey, versioningStorageConfig, storageContextUnitOfWork)
        {
        }

        /// <summary>
        /// Get the EF module namespace.
        /// </summary>
        public override string GetEFDatabaseName
        {
            get { return "VersioningDB"; }
        }

        /// <summary>
        /// Create a session, base class will take care of management.
        /// </summary>
        /// <returns></returns>
        public override IStorageServiceSession CreateNonManagedSession()
        {
            VersioningDB context = new VersioningDB(ProviderConnectionString);

            DbContextTransaction transaction = context.Database.BeginTransaction();
            EntityStorageSession session = new EntityStorageSession(context, transaction);
            return session;
        }

    }
}
