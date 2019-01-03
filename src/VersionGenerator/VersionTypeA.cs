using System;

namespace VersionGenerator
{
    public static class VersionTypeA
    {
        private static DateTimeOffset Epoch { get; } = new DateTimeOffset(2000, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

        public static Version GenerateFromTimestamp(DateTimeOffset timestamp, int major, int minor)
        {
            var utcTimestamp = timestamp.ToUniversalTime();
            var daysPart = (utcTimestamp - Epoch).TotalDays;
            var secondsPart = utcTimestamp.TimeOfDay.TotalSeconds / 2;

            return new Version(
                major: major,
                minor: minor,
                build: Convert.ToInt32(daysPart),
                revision: Convert.ToInt32(secondsPart));
        }
    }
}
