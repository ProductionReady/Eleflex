using System.Data.Entity;

namespace Eleflex.Storage.EF
{
    /// <summary>
    /// SQL Server Entity Framework session implementation.
    /// </summary>
    public partial class EntityStorageSession : StorageServiceSession
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityStorageSession() : base() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="transaction"></param>
        public EntityStorageSession(DbContext session, DbContextTransaction transaction) : base()
        {
            Session = session;
            Transaction = transaction;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            IsActive = false;
            if (Session != null)
                ((DbContext)Session).Dispose();
            if (Transaction != null)
                ((DbContextTransaction)Transaction).Dispose();

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
                ((DbContextTransaction)Transaction).Rollback();
        }

        /// <summary>
        /// Commit the changes.
        /// </summary>
        public override void Commit()
        {
            IsActive = false;
            if (Session != null)
                ((DbContext)Session).SaveChanges();
            if (Transaction != null && ((DbContextTransaction)Transaction).UnderlyingTransaction.Connection != null)
                ((DbContextTransaction)Transaction).Commit();
        }

    }
}
