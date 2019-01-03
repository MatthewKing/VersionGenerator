using System;

namespace VersionGenerator
{
    public static class VersionTypeC
    {
        public static Version GenerateFromTimestamp(DateTimeOffset timestamp, int major)
        {
            var utcTimestamp = timestamp.ToUniversalTime();
            var startOfYear = new DateTimeOffset(utcTimestamp.Year, 1, 1, 0, 0, 0, TimeSpan.Zero);

            return new Version(
                major: major,
                minor: utcTimestamp.Year - 2000,
                build: 10000 + Convert.ToInt16((utcTimestamp - startOfYear).TotalHours));
        }
    }
}
