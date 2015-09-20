namespace Eleflex
{
    /// <summary>
    /// Represents an object that maintains the overall state of work being processed over the request lifetime.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Commit the changes.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollback the changes.
        /// </summary>
        void Rollback();
    }
}
