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
        
        /// <summary>
        /// Fast remove method with O(1) complexity. Do not use it if an elements' order matters  
        /// </summary>
        /// <param name="list">The current list.</param>
        /// <param name="index">Index of the element.</param>
        public static void RemoveBySwap<T>(this List<T> list, int index)
        {
            list[index] = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
        }

        /// <summary>
        /// Fast remove method with O(n) complexity. Do not use it if an elements' order matters  
        /// </summary>
        /// <param name="list">The current list.</param>
        /// <param name="item">An element to be removed.</param>
        public static void RemoveBySwap<T>(this List<T> list, T item)
        {
            int index = list.IndexOf(item);
            RemoveBySwap(list, index);
        }

        /// <summary>
        /// Fast remove method with O(n) complexity. Do not use it if an elements' order matters  
        /// </summary>
        /// <param name="list">The current list.</param>
        /// <param name="predicate">An element evaluation predicate.</param>
        public static void RemoveBySwap<T>(this List<T> list, Predicate<T> predicate)
        {
            int index = list.FindIndex(predicate);
            RemoveBySwap(list, index);
        }
    }
}
