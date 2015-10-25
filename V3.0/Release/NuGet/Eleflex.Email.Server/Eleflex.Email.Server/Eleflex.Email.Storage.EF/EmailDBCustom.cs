using System.Data.Entity;

namespace Eleflex.Email.Server
{
    /// <summary>
    /// Represents an Entity Framework database context. This additional constructor allows us to pass in an additional connection string argument.
    /// </summary>
    public partial class EmailDB : DbContext
    {

        /// <summary>
        /// Constructor with param for connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        public EmailDB(string connectionString)
            : base(connectionString)
        {}

    }
}
