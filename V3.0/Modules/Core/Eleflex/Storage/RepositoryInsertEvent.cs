namespace Eleflex
{
    /// <summary>
    /// Represents a business rule event for a repository insert event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryInsertEvent<T> : BusinessRuleEvent, IItem<T>
    {

        /// <summary>
        /// The item object.
        /// </summary>
        public T Item { get; set; }

    }
}
