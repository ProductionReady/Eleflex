using System.Data.Entity;
using Eleflex.Storage.EF;
using Eleflex.Email.Server;
using Eleflex;

namespace Eleflex.Email.Storage.EF
{
    /// <summary>
    /// Provides abstraction to the Email storage mechanism.
    /// </summary>
    public partial class EmailStorageService : EntityStorageService, IEmailStorageService
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionStringKey"></param>
		/// <param name="versioningStorageConfig"></param>
        /// <param name="storageContextUnitOfWork"></param>
        public EmailStorageService(string connectionStringKey, string versioningStorageConfig, IStorageContextUnitOfWork storageContextUnitOfWork)
            : base(EmailConstants.STORAGE_SERVICE_NAME, connectionStringKey, versioningStorageConfig, storageContextUnitOfWork)
        {
        }

        /// <summary>
        /// Get the EF module namespace.
        /// </summary>
        public override string GetEFDatabaseName
        {
            get { return "EmailDB"; }
        }

        /// <summary>
        /// Create a session, base class will take care of management.
        /// </summary>
        /// <returns></returns>
        public override IStorageServiceSession CreateNonManagedSession()
        {
            EmailDB context = new EmailDB(ProviderConnectionString);

            DbContextTransaction transaction = context.Database.BeginTransaction();
            EntityStorageSession session = new EntityStorageSession(context, transaction);
            return session;
        }

        /// <summary>
        /// Create a session, base class will take care of management.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageServiceSession CreateNonManagedSession(TransactionIsolationLevel level)
        {
            EmailDB context = new EmailDB(ProviderConnectionString);
            System.Data.IsolationLevel isolationLevel = (System.Data.IsolationLevel)System.Data.IsolationLevel.Parse(typeof(TransactionIsolationLevel), level.ToString(), true);
            DbContextTransaction transaction = context.Database.BeginTransaction(isolationLevel);
            EntityStorageSession session = new EntityStorageSession(context, transaction);
            return session;
        }

    }
}
