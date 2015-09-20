namespace Eleflex
{
    /// <summary>
    /// Represents information that can be transmitted.
    /// </summary>
    public partial class SerializableData : ISerializableData
    {

        /// <summary>
        /// The key.
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        public virtual string Value { get; set; }
    }
}
