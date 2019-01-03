using System;

namespace VersionGenerator
{
    public static class VersionTypeB
    {
        public static Version GenerateFromTimestamp(DateTimeOffset timestamp)
        {
            var utcTimestamp = timestamp.ToUniversalTime();
            var startOfMonth = new DateTimeOffset(utcTimestamp.Year, utcTimestamp.Month, 1, 0, 0, 0, TimeSpan.Zero);

            return new Version(
                major: utcTimestamp.Year - 2000,
                minor: utcTimestamp.Month,
                build: 10000 + (int)((utcTimestamp - startOfMonth).TotalMinutes));
        }
    }
}
