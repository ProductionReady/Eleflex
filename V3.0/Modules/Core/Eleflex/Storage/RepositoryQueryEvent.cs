namespace Eleflex
{
    /// <summary>
    /// Represents a business rule event for a repository delete event.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TPkDataType"></typeparam>
    public partial class RepositoryQueryEvent<TObject> : BusinessRuleEvent, IItem<IStorageQuery>
    {

        /// <summary>
        /// The object.
        /// </summary>
        public IStorageQuery Item { get; set; }        

    }
}
