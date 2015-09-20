using System.Data.Entity;

namespace Eleflex.Versioning.Storage.EF
{
    /// <summary>
    /// Represents an Entity Framework database context. This additional constructor allows us to pass in an additional connection string argument.
    /// </summary>
    public partial class VersioningDB : DbContext
    {

        /// <summary>
        /// Constructor with param for connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        public VersioningDB(string connectionString)
            : base(connectionString)
        {}

    }
}
