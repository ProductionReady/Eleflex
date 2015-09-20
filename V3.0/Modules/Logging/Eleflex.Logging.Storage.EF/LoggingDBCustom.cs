using System.Data.Entity;

namespace Eleflex.Logging.Storage.EF
{
    /// <summary>
    /// Represents an Entity Framework database context. This additional constructor allows us to pass in an additional connection string argument.
    /// </summary>
    public partial class LoggingDB : DbContext
    {

        /// <summary>
        /// Constructor with param for connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        public LoggingDB(string connectionString)
            : base(connectionString)
        {}

    }
}
