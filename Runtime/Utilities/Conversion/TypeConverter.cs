using System;

namespace StansAssets.Foundation
{
    class TypeConverter<TSource, TDestination> : ITypeConverter<TSource, TDestination>
    {
        readonly Func<TSource, TDestination> m_Conversion;
        
        public TypeConverter(Func<TSource, TDestination> conversion)
        {
            m_Conversion = conversion;
        }

        public TDestination Convert(object value)
        {
            return m_Conversion((TSource) value);
        }

        public TDestination Convert(TSource value)
        {
            return m_Conversion(value);
        }
    }
    
    static class TypeConversion<TSource, TDestination>
    {
        static ITypeConverter<TSource, TDestination> s_Converter;

        /// <summary>
        /// Registers a strongly typed converter
        /// </summary>
        /// <param name="converter"></param>
        /// <typeparam name="TSource"></typeparam>
        internal static void Register(ITypeConverter<TSource, TDestination> converter)
        {
            s_Converter = converter;
        }
        
        /// <summary>
        /// Unregister the given type converter
        /// </summary>
        internal static void Unregister()
        {
            s_Converter = null;
        }

        /// <summary>
        /// Fast conversion path where types are known ahead of time
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TDestination Convert(TSource value)
        {
            return s_Converter.Convert(value);
        }
    }
}
