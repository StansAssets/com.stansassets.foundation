using System;
using System.Collections.Generic;

namespace StansAssets.Foundation
{
    /// <summary>
    /// Type conversion instance.
    /// Use to define own conversion flow on the instance level.
    ///
    /// If you would like to define global conversion see the <see cref="TypeConversion"/>
    /// </summary>
    public class TypeConversionInstance
    {
        // maybe using dynamic in this case would be better
        // but as of 2019.4 there is a known issue with dynamic not supported on iOS
        readonly Dictionary<Type,  Dictionary<Type, object>> m_ConvertersDictionary = new Dictionary<Type,  Dictionary<Type, object>>();
        
        
        /// <summary>
        /// Registers a new type conversion from the given source type to the given destination type.
        /// </summary>
        /// <param name="conversion">Conversion delegate method.</param>
        /// <typeparam name="TSource">Input type.</typeparam>
        /// <typeparam name="TDestination">Output type.</typeparam>
        public void Register<TSource, TDestination>(Func<TSource, TDestination> conversion)
        {
            if (typeof(TSource) == typeof(TDestination))
            {
                throw new ArgumentException(
                    $"Failed to register {nameof(TSource)} conversion method, source type and destination are the same.");
            }

            var converter = new TypeConverter<TSource, TDestination>(conversion);

            var type = typeof(TSource);
            if(!m_ConvertersDictionary.TryGetValue(type, out var dict))
            {
                dict = new Dictionary<Type, dynamic>();
                m_ConvertersDictionary.Add(type, dict);
            }

            if (dict.ContainsKey(type))
            {
                throw new ArgumentException($"Func<{typeof(TSource)}, {typeof(TDestination)}> has already been registered", 
                    nameof(conversion));
            }
            dict[typeof(TDestination)] = converter;
        }

        /// <summary>
        /// Methods is used to check of convertor is registered for a specified types pare.
        /// </summary>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Conversion Destination type.</typeparam>
        /// <returns>Returns `true` if convertor is registered, `false` otherwise.</returns>
        public bool HasConvertor<TSource, TDestination>()
        {
            return HasConvertor(typeof(TSource), typeof(TDestination));
        }

        /// <summary>
        /// Methods is used to check of convertor is registered for a specified types pare.
        /// </summary>
        /// <param name="source">Source type.</param>
        /// <param name="destination">Conversion Destination type.</param>
        /// <returns>Returns `true` if convertor is registered, `false` otherwise.</returns>
        public bool HasConvertor(Type source, Type destination)
        {
            if (m_ConvertersDictionary.TryGetValue(source, out var typeConvertors))
            {
                return typeConvertors.ContainsKey(destination);
            }

            return false;
        }
        
        /// <summary>
        /// Converts given the value to the destination type
        /// 
        /// @NOTE Fastest conversion method
        /// </summary>
        /// <param name="value">Value to convert to.</param>
        /// <typeparam name="TSource">Source type.</typeparam>
        /// <typeparam name="TDestination">Conversion Destination type.</typeparam>
        /// <returns>Converted value.</returns>
        public TDestination Convert<TSource, TDestination>(TSource value)
        {
            var typeConvertors = m_ConvertersDictionary[typeof(TSource)];
            var convertor = (TypeConverter<TSource, TDestination>) typeConvertors[typeof(TDestination)];
            return convertor.Convert(value);
        }
    }
}
