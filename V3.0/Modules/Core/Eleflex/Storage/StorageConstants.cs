namespace Eleflex
{
    /// <summary>
    /// Static class containing constants for the storage module.
    /// </summary>
    public static partial class StorageConstants
    {

        /// <summary>
        /// Connection string key for the default eleflex database instance.
        /// </summary>
        public const string CONNECTIONSTRING_KEY_DEFAULT = "EleflexDefault";

        /// <summary>
        /// The default maximum number of records.
        /// </summary>
        public const int MAX_RETURNED_RECORDS_DEFAULT = 500;

        /// <summary>
        /// The constuctor parameter for a storage service object specifiying the connection string argument.
        /// </summary>
        public const string ISTORAGESERVICE_CONSTUCTORPARAM_CONNECTIONSTRING = "connectionStringKey";

        /// <summary>
        /// The constuctor parameter for a storage service object specifiying the versioning storage config argument.
        /// </summary>
        public const string ISTORAGESERVICE_CONSTUCTORPARAM_VERSIONINGSTORAGECONFIG = "versioningStorageConfig";

    }
}
