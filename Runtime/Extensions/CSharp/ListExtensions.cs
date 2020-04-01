using System;
using System.Collections.Generic;
using System.Linq;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// CSharp List extension methods.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Resizes the list. In case of increase list size - fills it with default elements.
        /// </summary>
        /// <param name="list">The current list.</param>
        /// <param name="newSize">The new size of the list.</param>
        /// <param name="defaultValue">The default value to set as new list elements.</param>
        public static void Resize<T>(this List<T> list, int newSize, T defaultValue = default(T))
        {
            int currentSize = list.Count;
            if (newSize < currentSize)
            {
                list.RemoveRange(newSize, currentSize - newSize);
            }
            else if (newSize > currentSize)
            {
                if (newSize > list.Capacity)
                    list.Capacity = newSize;

                list.AddRange(Enumerable.Repeat(defaultValue, newSize - currentSize));
            }
        }

        /// <summary>
        /// Creates a deep copy of the list.
        /// </summary>
        /// <param name="list">The current list.</param>
        /// <returns>The deep copy of the current list.</returns>
        public static List<T> Clone<T>(this List<T> list) where T : ICloneable
        {
            return list.Select(item => (T)item.Clone()).ToList();
        }

        /// <summary>
        /// Creates a shallow copy of the list.
        /// </summary>
        /// <param name="list">The current list.</param>
        /// <returns>The shallow copy of the current list.</returns>
        public static List<T> ShallowCopy<T>(this List<T> list)
        {
            return list.ToList();
        }
    }
}
