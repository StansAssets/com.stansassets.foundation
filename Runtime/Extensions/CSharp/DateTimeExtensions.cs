using System;

namespace StansAssets.Foundation.Extensions
{
    /// <summary>
    /// CSharp <see cref="DateTime"/> extension methods.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts unix timestamp to <see cref="DateTime"/> with high precision.
        /// </summary>
        /// <param name="unixTime">Unix timestamp.</param>
        /// <returns>DateTime object that represents the same moment in time as provided Unix time.</returns>
        public static DateTime UnixTimestampToDateTime(double unixTime)
        {
            var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unixTimeStampInTicks = (long) (unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, System.DateTimeKind.Utc);
        }
        
        /// <summary>
        /// Converts <see cref="DateTime"/> to unix timestamp with high precision
        /// </summary>
        /// <param name="dateTime">DateTime date representation.</param>
        /// <returns>unix timestamp that represents the same moment in time as provided DateTime object.</returns>
        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var unixTimeStampInTicks = (dateTime.ToUniversalTime() - unixStart).Ticks;
            return (double) unixTimeStampInTicks / TimeSpan.TicksPerSecond;
        }
    }
}
