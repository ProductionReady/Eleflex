namespace Eleflex
{
    /// <summary>
    /// Represets an object repository that perform business rule processing and storage. This is the main repository that all domain objects should use.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TPkDataType"></typeparam>
    public partial interface IBusinessRepository<TObject, TPkDataType> : IRepository<TObject, TPkDataType>
        where TObject : class
    {
    }
}