using System;
using System.Collections.Generic;
using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Retrieves specified symbols amount from the end of the string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="count">Amount of symbols</param>
        /// <returns>Specified symbols amount from the end of the string.</returns>
        public static string GetLast(this string source, int count)
        {
            return count >= source.Length ? source : source.Substring(source.Length - count);
        }

        /// <summary>
        /// Removes specified symbols amount from the end of the string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="count">Amount of symbols</param>
        /// <returns>Modified string.</returns>
        public static string RemoveLast(this string source, int count)
        {
            return count >= source.Length ? string.Empty : source.Remove(source.Length - count);
        }

        /// <summary>
        /// Retrieves specified symbols amount from the beginning of the string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="count">Amount of symbols</param>
        /// <returns>Specified symbols amount from the beginning of the string.</returns>
        public static string GetFirst(this string source, int count)
        {
            return count >= source.Length ? source : source.Substring(0, count);
        }

        /// <summary>
        /// Method will return all the indexes for a matched string.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <param name="value">String Value to look for.</param>
        /// <param name="comparisonType">Comparison Type.</param>
        /// <returns>Indexes for a matched string.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<int> AllIndexesOf(this string source, string value, StringComparison comparisonType)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", nameof(value));

            var indexes = new List<int>();
            for (var index = 0;; index += value.Length)
            {
                index = source.IndexOf(value, index, comparisonType);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }

        /// <summary>
        /// Copy specified string to the system copy buffer.
        /// </summary>
        /// <param name="source">>Source string.</param>
        public static void CopyToClipboard(this string source)
        {
            var textEditor = new TextEditor { text = source };
            textEditor.SelectAll();
            textEditor.Copy();
        }
    }
}
