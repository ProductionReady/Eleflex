namespace Eleflex
{
    /// <summary>
    /// Represents an object for repositories that map to another object repository. Primary keys must be identical between the source and destination objects.
    /// </summary>
    /// <typeparam name="TSourceObject"></typeparam>
    /// <typeparam name="TSourcePkDataType"></typeparam>
    /// <typeparam name="TDestinationObject"></typeparam>
    /// <typeparam name="TDestinationRepositoryType"></typeparam>
    public partial interface IMappingRepository<TSourceObject, TSourcePkDataType, TDestinationObject, TDestinationRepositoryType> : IRepository<TSourceObject, TSourcePkDataType>
        where TSourceObject : class
        where TDestinationObject : class
    {
    }
}
