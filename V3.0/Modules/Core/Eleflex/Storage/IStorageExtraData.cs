namespace Eleflex
{
    /// <summary>
    /// Represents an interface that provides a quick and easy extension property for integrators to store and retrieve customized 
    /// data without having to change the data model. Possible uses would be serialize multiple properties as json and store in the value.
    /// </summary>
    public partial interface IStorageExtraData
    {
        /// <summary>
        /// Property providing an extension for customized information.
        /// </summary>
        string ExtraData { get; set; }
    }
}
