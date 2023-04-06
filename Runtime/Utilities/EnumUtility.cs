using System;

namespace StansAssets.Foundation
{
    /// <summary>
    /// Enum Utility Methods.
    /// </summary>
    public static class EnumUtility
    {
        /// <summary>
        /// Checks if the string representation of the name or numeric value of one or more enumerated constants can be converted to an equivalent enumerated object.
        /// </summary>
        /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
        /// <param name="ignoreCase"><c>true</c> to read <paramref name="value"/> in case insensitive mode; <c>false</c> to read <paramref name="value"/> in case sensitive mode.</param>
        /// <typeparam name="T">The enum type to use for parsing.</typeparam>
        /// <returns>Returns <c>true</c> if the parsed <paramref name="value"/> can be converted to an equivalent enumerated object.</returns>
        public static bool CanBeParsed<T>(string value, bool ignoreCase = true) where T : struct, Enum
        {
            return Enum.TryParse(value, ignoreCase, out T _);
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
        /// <param name="result">When this method returns <c>true</c>, contains an enumeration constant that represents the parsed <paramref name="value"/>.</param>
        /// <param name="ignoreCase"><c>true</c> to read <paramref name="value"/> in case insensitive mode; <c>false</c> to read <paramref name="value"/> in case sensitive mode.</param>
        /// <typeparam name="T">The enum type to use for parsing.</typeparam>
        /// <returns><c>true</c> if the conversion succeeded; <c>false</c> otherwise.</returns>
        public static bool TryParse<T>(string value, out T result, bool ignoreCase = true) where T : struct, Enum
        {
            return Enum.TryParse(value, ignoreCase, out result);
        }

        /// <summary>
        /// Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.
        /// </summary>
        /// <param name="value">The string representation of the name or numeric value of one or more enumerated constants.</param>
        /// <param name="ignoreCase"><c>true</c> to read <paramref name="value"/> in case insensitive mode; <c>false</c> to read <paramref name="value"/> in case sensitive mode.</param>
        /// <typeparam name="T">The enum type to use for parsing.</typeparam>
        /// <returns>Enumeration constant that represents the parsed <paramref name="value"/> if it can be converted to an equivalent enumerated object. If not, returns default value of type <typeparamref name="T"/></returns>
        public static T ParseOrDefault<T>(string value, bool ignoreCase = true) where T : struct, Enum
        {
            if (!Enum.TryParse(value, ignoreCase, out T result))
            {
                result = default;
            }

            return result;
        }
    }
}
