namespace Eleflex
{
    /// <summary>
    /// Represents a business rule event for a repository update event.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class RepositoryUpdateEvent<T> : BusinessRuleEvent, IItem<T>
    {

        /// <summary>
        /// The item object.
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// The previous item object.
        /// </summary>
        public T PreviousItem { get; set; }

    }
}
