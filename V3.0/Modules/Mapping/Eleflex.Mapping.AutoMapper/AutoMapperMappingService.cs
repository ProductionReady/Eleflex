using System;
using System.Collections.Generic;
using AutoMapper;

namespace Eleflex.Mapping.AutoMapper
{
    /// <summary>
    /// Represents an object that can map between two objects using AutoMapper.
    /// </summary>
    public partial class AutoMapperMappingService : IMappingService
    {

        /// <summary>
        /// Map from one type to another.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual T2 Map<T1, T2>(T1 item)
        {
            return Mapper.Map<T1, T2>(item);
        }

        /// <summary>
        /// Map from one type to another using an existing object.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public virtual T2 Map<T1, T2>(T1 source, T2 destination)
        {
            return Mapper.Map<T1, T2>(source, destination);
        }

        /// <summary>
        /// Map from one type to another.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual T1 Map<T1, T2>(T2 item)
        {
            return Mapper.Map<T2, T1>(item);            
        }

        /// <summary>
        /// Map from one type to another using an existing object.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public virtual T1 Map<T1, T2>(T2 source, T1 destination)
        {
            return Mapper.Map<T2, T1>(source, destination);
        }

        /// <summary>
        /// Map a list of one type to a list of another.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual IList<T2> Map<T1, T2>(IList<T1> item)
        {
            return Mapper.Map<IList<T1>, IList<T2>>(item);
        }

        /// <summary>
        /// Map a list of one type to a list of another.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual IList<T1> Map<T1, T2>(IList<T2> item)
        {
            return Mapper.Map<IList<T2>, IList<T1>>(item);
        }

        /// <summary>
        /// Map from one type to another.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public virtual object Map(object source, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, sourceType, destinationType);
        }

        /// <summary>
        /// Map from one type to another using an existing object.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public virtual object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            return Mapper.Map(source, destination, sourceType, destinationType);
        }

    }
}
