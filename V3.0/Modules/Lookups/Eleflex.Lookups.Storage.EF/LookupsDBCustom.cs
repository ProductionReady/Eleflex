using System.Data.Entity;

namespace Eleflex.Lookups.Storage.EF
{
    /// <summary>
    /// Represents an Entity Framework database context. This additional constructor allows us to pass in an additional connection string argument.
    /// </summary>
    public partial class LookupsDB : DbContext
    {

        /// <summary>
        /// Constructor with param for connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        public LookupsDB(string connectionString)
            : base(connectionString)
        {}

    }
}
