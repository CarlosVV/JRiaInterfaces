namespace CES.CoreApi.Common.Interfaces
{
    public interface IAutoMapperProxy
    {
        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// </summary>
        /// <returns>
        /// Mapped destination object
        /// </returns>
        TDestination Map<TSource, TDestination>(TSource source);
    }
}