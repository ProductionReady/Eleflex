using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex
{
    //
    // Summary:
    //     Specifies the transaction locking behavior for the connection.
    public enum TransactionIsolationLevel
    {
        //
        // Summary:
        //     A different isolation level than the one specified is being used, but the level
        //     cannot be determined.
        Unspecified = -1,
        //
        // Summary:
        //     The pending changes from more highly isolated transactions cannot be overwritten.
        Chaos = 16,
        //
        // Summary:
        //     A dirty read is possible, meaning that no shared locks are issued and no exclusive
        //     locks are honored.
        ReadUncommitted = 256,
        //
        // Summary:
        //     Shared locks are held while the data is being read to avoid dirty reads, but
        //     the data can be changed before the end of the transaction, resulting in non-repeatable
        //     reads or phantom data.
        ReadCommitted = 4096,
        //
        // Summary:
        //     Locks are placed on all data that is used in a query, preventing other users
        //     from updating the data. Prevents non-repeatable reads but phantom rows are still
        //     possible.
        RepeatableRead = 65536,
        //
        // Summary:
        //     A range lock is placed on the System.Data.DataSet, preventing other users from
        //     updating or inserting rows into the dataset until the transaction is complete.
        Serializable = 1048576,
        //
        // Summary:
        //     Reduces blocking by storing a version of data that one application can read while
        //     another is modifying the same data. Indicates that from one transaction you cannot
        //     see changes made in other transactions, even if you requery.
        Snapshot = 16777216
    }
}
