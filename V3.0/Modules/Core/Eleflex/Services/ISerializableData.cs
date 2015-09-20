namespace Eleflex
{
    /// <summary>
    /// Represents information that can be transmitted.
    /// </summary>
    public partial interface ISerializableData
    {

        /// <summary>
        /// The key.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        string Value { get; set; }

    }
}
