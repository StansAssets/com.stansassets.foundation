using System;

namespace StansAssets.Foundation.Editor
{
    /// <summary>
    /// Helper methods to work with unix time representation.
    /// </summary>
    public static class UnixTime
    {
        /// <summary>
        /// Converts a UNIX time stamp into <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="timestamp">UNIX timestamp.</param>
        public static DateTime ToDateTime(long timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }

        /// <summary>
        /// Gets a UNIX timestamp from a <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="date">Source date for conversion.</param>
        public static long ToUnixTime(DateTime date)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var diff = date.ToUniversalTime() - origin;
            return (long)diff.TotalSeconds;
        }
    }
}
