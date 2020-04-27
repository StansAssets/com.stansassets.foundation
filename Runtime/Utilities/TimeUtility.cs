using System;
using System.Collections.Generic;

namespace StansAssets.Foundation
{
    /// <summary>
    /// Time Related Utility Methods.
    /// </summary>
    public static class TimeUtility
    {
        static readonly Dictionary<string, long> s_Timers = new Dictionary<string, long>();

        /// <summary>
        /// Start new timer, with given name.
        /// You can check timer second value via <see cref="GetTime"/> method.
        /// </summary>
        /// <param name="name">Timer name.</param>
        public static void StartTimer(string name)
        {
            s_Timers[name] = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Get timer value in seconds by timer name. You may star any number of timers using the <see cref="StartTimer"/> method.
        /// If timer with specified name doesn't exist `0` value will be returned.
        /// </summary>
        /// <param name="name">Timer name.</param>
        /// <returns>Timer value in seconds</returns>
        public static float GetTime(string name)
        {
            if (s_Timers.TryGetValue(name, out var startTicksValue))
            {
                var ticks = DateTime.Now.Ticks - startTicksValue;
                return (float)ticks / TimeSpan.TicksPerSecond;
            }

            return 0f;
        }


        /// <summary>
        /// Converts a UNIX time stamp into <see cref="DateTime"/> object.
        /// </summary>
        /// <param name="timestamp">UNIX timestamp.</param>
        public static DateTime FromUnixTime(long timestamp)
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
