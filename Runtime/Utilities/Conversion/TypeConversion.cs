using System;
using System.Collections.Generic;

namespace StansAssets.Foundation
{
    static class TypeConversion<TDestination>
    {
        static readonly Dictionary<Type, ITypeConverter<TDestination>> s_Converters = new Dictionary<Type, ITypeConverter<TDestination>>();
        
        /// <summary>
        /// Registers a new type converter
        /// </summary>
        /// <param name="converter"></param>
        /// <typeparam name="TSource"></typeparam>
        internal static void Register<TSource>(ITypeConverter<TSource, TDestination> converter)
        {
            var type = typeof(TSource);
            if (s_Converters.ContainsKey(type))
            {
                throw new ArgumentException($"TypeConverter<{typeof(TSource)}, {typeof(TDestination)}> has already been registered", 
                    nameof(converter));
            }

            s_Converters[typeof(TSource)] = converter;
        }

        /// <summary>
        /// Unregisters the converter from TSource to TDestination.
        /// </summary>
        internal static void Unregister<TSource>()
        {
            s_Converters.Remove(typeof(TSource));
        }

        /// <summary>
        /// Unregister all type converters
        /// </summary>
        internal static void UnregisterAll()
        {
            s_Converters.Clear();
        }

        /// <summary>
        /// Generic conversion path where destination type is known ahead of time and source type must be resolved
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TDestination Convert(object value)
        {
            var type = value.GetType();
            var converter = GetTypeConverter(type);

            if (null == converter)
            {
                throw new NullReferenceException($"No TypeConverter<,> has been registered to convert '{type}' to '{typeof(TDestination)}'");
            }

            return converter.Convert(value);
        }

        /// <summary>
        /// Generic conversion path where destination type is known ahead of time and source type must be resolved
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns>Returns `true` if conversion is succeeded.</returns>
        public static bool TryConvert(object value, out TDestination result)
        {
            var type = value.GetType();
            var converter = GetTypeConverter(type);

            if (null == converter)
            {
                result = default;
                return false;
            }

            result = converter.Convert(value);
            return true;
        }

        /// <summary>
        /// Enumerates the class hierarchy and returns the first converter found
        ///
        /// @NOTE This method will resolve base classes before interfaces
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Type converter for a provided class.</returns>
        static ITypeConverter<TDestination> GetTypeConverter(Type type)
        {
            var t = type;
            while (null != t)
            {
                if (s_Converters.TryGetValue(t, out var converter))
                {
                    return converter;
                }

                t = t.BaseType;
            }

            var interfaces = type.GetInterfaces();
            foreach (var i in interfaces)
            {
                if (s_Converters.TryGetValue(i, out var converter))
                {
                    return converter;
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Global TypeConversion API.
    /// @Warning only use it on application level. Do not use it in the package or plugin.
    /// For package & plugins usage see <see cref="TypeConversionInstance"/>.
    /// </summary>
    public static class TypeConversion
    {
        static readonly HashSet<Action> s_UnregisterMethods = new HashSet<Action>();
        
        /// <summary>
        /// Registers a new type conversion from the given source type to the given destination type.
        /// </summary>
        /// <param name="conversion">Conversion delegate method.</param>
        /// <typeparam name="TSource">Input type.</typeparam>
        /// <typeparam name="TDestination">Output type.</typeparam>
        public static void Register<TSource, TDestination>(Func<TSource, TDestination> conversion)
        {
            if (typeof(TSource) == typeof(TDestination))
            {
                throw new ArgumentException(
                    $"Failed to register {nameof(TSource)} conversion method, source type and destination are the same.");
            }

            var converter = new TypeConverter<TSource, TDestination>(conversion);
            
            TypeConversion<TSource, TDestination>.Register(converter);
            TypeConversion<TDestination>.Register(converter);

            s_UnregisterMethods.Add(TypeConversion<TSource, TDestination>.Unregister);
            s_UnregisterMethods.Add(TypeConversion<TDestination>.UnregisterAll);
        }

        /// <summary>
        /// Unregisters the converter for the given (TSource, TDestination) pair.
        /// </summary>
        public static void Unregister<TSource, TDestination>()
        {
            // remove specific converter if any
            TypeConversion<TSource, TDestination>.Unregister();
            
            // remove generic converter if any
            TypeConversion<TDestination>.Unregister<TSource>();
        }

        static bool IsNull<TValue>(TValue value)
        {
            if (ReferenceEquals(value, null))
            {
                return true;
            }

            // Handle fake nulls
            var obj = value as UnityEngine.Object;
            return null != obj && !obj;
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
        public static TDestination Convert<TSource, TDestination>(TSource value)
        {
            return TypeConversion<TSource, TDestination>.Convert(value);
        }

        /// <summary>
        /// Try convert the given value to the destination type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static bool TryConvert<TValue>(object value, out TValue result)
        {
            try
            {
                result = Convert<TValue>(value);
                return true;
            }
            catch
            {
                // ignored
            }

            result = default;
            return false;
        }
        
        /// <summary>
        /// Converts given the value to the destination type
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="TDestination"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static TDestination Convert<TDestination>(object source)
        {
            // Try a straightforward cast, this is always the best case scenario
            if (source is TDestination destination)
            {
                return destination;
            }

            if (typeof(TDestination).IsValueType)
            {
                // There is no elegant default behaviour we can do here, we must throw in this case
                if (source == null)
                {
                    throw new Exception($"Failed to convert from 'null' to '{typeof(TDestination)}' value is null.");
                }
            }
            else if (IsNull(source))
            {
                return default;
            }

            // Try to forward to a user defined implementation for conversion
            if (TypeConversion<TDestination>.TryConvert(source, out var result))
            {
                return result;
            }
            
            // At this point we can try our best to convert the value
            
            // Special handling of enum types
            if (typeof(TDestination).IsEnum)
            {
                if (source is string s)
                {
                    return (TDestination) Enum.Parse(typeof(TDestination), s, true);
                }

                // Try to convert to the underlying type
                var v = System.Convert.ChangeType(source, Enum.GetUnderlyingType(typeof(TDestination)));
                return (TDestination) v;
            }

            if (source is IConvertible)
            {
                return (TDestination) System.Convert.ChangeType(source, typeof(TDestination));
            }

            throw new Exception($"Failed to convert from '{source?.GetType()}' to '{typeof(TDestination)}'.");
        }

        /// <summary>
        /// Converts given the value to the destination type
        /// </summary>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Convert(object source, Type type)
        {
            if (type == source?.GetType())
            {
                return source;
            }
            
            if (type.IsValueType)
            {
                // There is no elegant default behaviour we can do here, we must throw in this case
                if (source == null)
                {
                    throw new Exception($"Failed to convert from 'null' to '{type}' value is null.");
                }
            }
            else if (IsNull(source))
            {
                return null;
            }
            
            
            // Try to forward to a user defined implementation for conversion
            // object result;

            var converter = typeof(TypeConversion<>).MakeGenericType(type);
            var method = converter.GetMethod("TryConvert");

            if (null != method)
            {
                var parameters = new [] {source, null};
                var result = method.Invoke(null, parameters);
                if ((bool) result)
                {
                    return parameters[1];
                }
            }
            
            // At this point we can try our best to convert the value
            
            // Special handling of enum types
            if (type.IsEnum)
            {
                return source is string s 
                    ? Enum.Parse(type, s) 
                    : System.Convert.ChangeType(source, Enum.GetUnderlyingType(type));
            }

            if (source is IConvertible)
            {
                return System.Convert.ChangeType(source, type);
            }

            throw new Exception($"Failed to convert from '{source?.GetType()}' to '{type}'.");
        }
    }
}