using System.Data.Entity;
using Eleflex.Storage.EF;

namespace Eleflex.Security.Storage.EF
{
    /// <summary>
    /// Provides abstraction to the Security storage mechanism.
    /// </summary>
    public partial class SecurityStorageService : EntityStorageService, ISecurityStorageService
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionStringKey"></param>
        /// <param name="versioningStorageConfig"></param>
        /// <param name="storageContextUnitOfWork"></param>
        public SecurityStorageService(string connectionStringKey, string versioningStorageConfig, IStorageContextUnitOfWork storageContextUnitOfWork)
            : base(SecurityConstants.STORAGE_SERVICE_NAME, connectionStringKey, versioningStorageConfig, storageContextUnitOfWork)
        {
        }

        /// <summary>
        /// Get the EF module namespace.
        /// </summary>
        public override string GetEFDatabaseName
        {
            get { return "Eleflex.Security.Storage.EF.SecurityDB"; }
        }

        /// <summary>
        /// Create a session, base class will take care of management.
        /// </summary>
        /// <returns></returns>
        public override IStorageServiceSession CreateNonManagedSession()
        {
            SecurityDB context = new SecurityDB(ProviderConnectionString);

            DbContextTransaction transaction = context.Database.BeginTransaction();
            EntityStorageSession session = new EntityStorageSession(context, transaction);
            return session;
        }

    }
}
