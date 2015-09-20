using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object that can map between two objects.
    /// </summary>
    public partial interface IMappingService
    {

        /// <summary>
        /// Map from one type to another.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        T2 Map<T1, T2>(T1 item);

        /// <summary>
        /// Map from one type to another using an existing object.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        T2 Map<T1, T2>(T1 source, T2 destination);

        /// <summary>
        /// Map from one type to another.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        T1 Map<T1, T2>(T2 item);

        /// <summary>
        /// Map from one type to another using an existing object.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        T1 Map<T1, T2>(T2 source, T1 destination);

        /// <summary>
        /// Map a list of one type to a list of another.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        IList<T2> Map<T1, T2>(IList<T1> item);

        /// <summary>
        /// Map a list of one type to a list of another.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        IList<T1> Map<T1, T2>(IList<T2> item);

        /// <summary>
        /// Map from one type to another.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        object Map(object source, Type sourceType, Type destinationType);

        /// <summary>
        /// Map from one type to another using an existing object.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        object Map(object source, object destination, Type sourceType, Type destinationType);


    }
}
