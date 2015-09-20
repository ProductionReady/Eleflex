namespace Eleflex
{
    /// <summary>
    /// Represents a business rule event for a repository delete event.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TPkDataType"></typeparam>
    public partial class RepositoryDeleteEvent<TObject, TPkDataType> : BusinessRuleEvent, IItem<TObject>
    {

        /// <summary>
        /// The object.
        /// </summary>
        public TObject Item { get; set; }

        /// <summary>
        /// The PK for the item.
        /// </summary>
        public TPkDataType PK { get; set; }

    }
}
