namespace StansAssets.Foundation
{
    /// <summary>
    /// Base interface to for a strongly typed conversion.
    /// </summary>
    /// <typeparam name="TDestination">Conversion Destination type.</typeparam>
    public interface ITypeConverter<out TDestination>
    {
        TDestination Convert(object value);
    }

    /// <inheritdoc />
    /// <summary>
    /// Interface to map a strong conversion between two types.
    /// </summary>
    /// <typeparam name="TSource">Conversion Source type.</typeparam>
    /// <typeparam name="TDestination">Conversion Destination type.</typeparam>
    public interface ITypeConverter<in TSource, out TDestination> : ITypeConverter<TDestination>
    {
    }
}
