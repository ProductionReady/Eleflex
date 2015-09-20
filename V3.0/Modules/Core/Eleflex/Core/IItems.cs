using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object containing a collection of typed object items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IItems<T>
    {

        /// <summary>
        /// The collection of typed object items.
        /// </summary>
        IList<T> Items { get; set; }
    }
}
