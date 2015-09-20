using System.Data.SqlClient;

namespace Eleflex.Storage.EF
{
    /// <summary>
    /// SQL Server generic session implementation.
    /// </summary>
    public partial class SqlStorageSession : StorageServiceSession
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SqlStorageSession() : base() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        public SqlStorageSession(SqlConnection connection, SqlTransaction transaction)
            : base()
        {
            Session = connection;
            Transaction = transaction;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            IsActive = false;
            if (Session != null)
                ((SqlConnection)Session).Dispose();
            if (Transaction != null)
                ((SqlTransaction)Transaction).Dispose();

            Session = null;
            Transaction = null;
            base.Dispose();
        }

        /// <summary>
        /// Rollback the changes.
        /// </summary>
        public override void Rollback()
        {
            IsActive = false;
            if (Transaction != null)
                ((SqlTransaction)Transaction).Rollback();
        }

        /// <summary>
        /// Commit the changes.
        /// </summary>
        public override void Commit()
        {
            IsActive = false;
            if (Transaction != null && ((SqlTransaction)Transaction).Connection != null)
                ((SqlTransaction)Transaction).Commit();
        }

    }
}
