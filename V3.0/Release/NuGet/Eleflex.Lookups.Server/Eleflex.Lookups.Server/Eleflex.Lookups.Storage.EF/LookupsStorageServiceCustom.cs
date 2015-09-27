using System.Data.Entity;
using Eleflex.Storage.EF;

namespace Eleflex.Lookups.Storage.EF
{
    /// <summary>
    /// Provides abstraction to the Lookups storage mechanism.
    /// </summary>
    public partial class LookupsStorageService : EntityStorageService, ILookupsStorageService
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionStringKey"></param>
        /// <param name="versioningStorageConfig"></param>
        /// <param name="storageContextUnitOfWork"></param>
        public LookupsStorageService(string connectionStringKey, string versioningStorageConfig, IStorageContextUnitOfWork storageContextUnitOfWork)
            : base(LookupsConstants.STORAGE_SERVICE_NAME, connectionStringKey, versioningStorageConfig, storageContextUnitOfWork)
        {
        }

        /// <summary>
        /// Get the EF module namespace.
        /// </summary>
        public override string GetEFDatabaseName
        {
            get { return "Eleflex.Lookups.Storage.EF.LookupsDB"; }
        }

        /// <summary>
        /// Create a session, base class will take care of management.
        /// </summary>
        /// <returns></returns>
        public override IStorageServiceSession CreateNonManagedSession()
        {
            LookupsDB context = new LookupsDB(ProviderConnectionString);

            DbContextTransaction transaction = context.Database.BeginTransaction();
            EntityStorageSession session = new EntityStorageSession(context, transaction);
            return session;
        }

    }
}
