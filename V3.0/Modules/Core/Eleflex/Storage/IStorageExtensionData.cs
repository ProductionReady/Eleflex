namespace Eleflex
{
    /// <summary>
    /// Represents that an object will store an additional piece of string information along with the storage object. 
    /// This allows developers a quick and easy way to extend pre-built models without having to change underlying models/mechanisms.
    /// </summary>
    public partial interface IStorageExtensionData
    {

        /// <summary>
        /// The additional extension data.
        /// </summary>
        string StorageExtensionData { get; set; }
    }
}
