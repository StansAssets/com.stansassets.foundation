using System;
using System.Collections.Generic;
using UnityEngine;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// String extension methods.
    /// </summary>
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
                throw new ArgumentException("The string to find should not be empty.", nameof(value));

            var index = 0;
            var indexResult = 0;
            var indexes = new List<int>();
            while (indexResult != -1)
            {
                indexResult = source.IndexOf(value, index, comparisonType);
                if(indexResult != -1)
                    indexes.Add(index);
                
                index++;
            }

            return indexes;
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

        /// <summary>
        /// Removes all the leading occurrences of specified string from the current string.
        /// </summary>
        /// <param name="target">Current string.</param>
        /// <param name="trimString">A string to remove.</param>
        /// <returns>The string that remains after all occurrences of trimString parameter are removed from the start of the current string.</returns>
        public static string TrimStart(this string target, string trimString)
        {
            if (string.IsNullOrEmpty(trimString)) return target;

            var result = target;
            while (result.StartsWith(trimString))
            {
                result = result.Substring(trimString.Length);
            }

            return result;
        }

        /// <summary>
        /// Removes all the trailing occurrences of specified string from the current string.
        /// </summary>
        /// <param name="target">Current string</param>
        /// <param name="trimString">A string to remove.</param>
        /// <returns>The string that remains after all occurrences of trimString parameter are removed from the end of the current string.</returns>
        public static string TrimEnd(this string target, string trimString)
        {
            if (string.IsNullOrEmpty(trimString)) return target;

            var result = target;
            while (result.EndsWith(trimString))
            {
                result = result.Substring(0, result.Length - trimString.Length);
            }

            return result;
        }
    }
}
