using AutoMapper;
using CES.CoreApi.Common.Interfaces;

namespace CES.CoreApi.Common.Proxies
{
    public class AutoMapperProxy : IAutoMapperProxy
    {
        /// <summary>
        /// Execute a mapping from the source object to a new destination object.
        /// </summary>
        /// <returns>
        /// Mapped destination object
        /// </returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}
