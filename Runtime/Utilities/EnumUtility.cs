using System;
using UnityEngine;

namespace StansAssets.Foundation
{
    /// <summary>
    /// Enum Utility Methods.
    /// </summary>
    public static class EnumUtility
    {
        /// <summary>
        /// Check's if a given string can be parsed to a specified enum type.
        /// </summary>
        /// <param name="value">String enum value.</param>
        public static bool CanBeParsed<T>(string value) where T : struct, Enum
        {
            try
            {
                var unused = Enum.Parse(typeof(T), value, true);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries to parse string value to a specified enum type.
        /// </summary>
        /// <param name="value">String enum value.</param>
        /// <param name="result">Enum result</param>
        public static bool TryParse<T>(string value, out T result) where T : struct, Enum
        {
            try
            {
                result = (T)Enum.Parse(typeof(T), value, true);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        /// <summary>
        /// Tries to parse string value to a specified enum type.
        /// Will print a warning in case of failure, and return default value for a given Enum type.
        /// </summary>
        /// <param name="value">String enum value.</param>
        public static T ParseOrDefault<T>(string value) where T : struct, Enum
        {
            try
            {
                var val = (T)Enum.Parse(typeof(T), value, true);
                return val;
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Enum Parsing failed: " + ex.Message);
                return default;
            }
        }
    }
}