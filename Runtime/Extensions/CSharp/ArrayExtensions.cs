namespace StansAssets.Foundation
{
    /// <summary>
    /// CSharp Array extension methods.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Returns a shallow copy of a portion of an current array as a new array object.
        /// </summary>
        /// <param name="data">A current array.</param>
        /// <param name="index">The index of the first copied element.</param>
        /// <param name="length">The number of elements to copy.</param>
        /// <returns>A new array containing the current array's elements that fall within the limits specified by index and length.</returns>
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            var result = new T[length];
            System.Array.Copy(data, index, result, 0, length);
            return result;
        }

        /// <summary>
        /// Swaps two elements in the current array.
        /// </summary>
        /// <param name="data">A current array.</param>
        /// <param name="index0">The index of the first element to swap.</param>
        /// <param name="index1">The index of the second element to swap.</param>
        public static void SwapElements<T>(this T[] data, int index0, int index1)
        {
            T t = data[index0];
            data[index0] = data[index1];
            data[index1] = t;
        }
    }
}
