using System.Data.SqlClient;
using Eleflex.Storage.EF;

namespace Eleflex.Versioning.Storage.EF
{
    /// <summary>
    /// Provides abstraction to logging storage mechanism.
    /// </summary>
    public partial class VersioningSqlStorageService : SqlStorageService, IVersioningStorageService
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connectionStringKey"></param>
        /// <param name="storageContextUnitOfWork"></param>
        public VersioningSqlStorageService(string connectionStringKey, IStorageContextUnitOfWork storageContextUnitOfWork)
            :base(VersioningConstants.STORAGE_SERVICE_NAME, connectionStringKey, storageContextUnitOfWork)
        {
        }

        /// <summary>
        /// Create a session, base class will take care of management.
        /// </summary>
        /// <returns></returns>
        public override IStorageServiceSession CreateNonManagedSession()
        {
            SqlConnection connection = new SqlConnection(ProviderConnectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            SqlStorageSession session = new SqlStorageSession(connection, transaction);
            return session;
        }

    }
}
