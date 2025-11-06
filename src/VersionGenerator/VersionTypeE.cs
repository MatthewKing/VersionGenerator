using System;

namespace VersionGenerator;

/// <summary>
/// Provides functionality to generate type-E versions.
/// </summary>
public static class VersionTypeE
{
    /// <summary>
    /// Generates a new type-E version.
    /// </summary>
    /// <param name="timestamp">The timestamp to be encoded in the version number.</param>
    /// <param name="major">The major version.</param>
    /// <param name="epoch">The major version epoch.</param>
    /// <returns>A type-E version.</returns>
    public static Version GenerateFromTimestamp(DateTimeOffset timestamp, int major, DateTimeOffset epoch)
    {
        // There are 31,556,926 seconds in a year.
        // We have 24 bits to represent the value, so 16,777,216 possible values.
        // If we let each value represent a 30 second interval, then we can represent just under 16 years.
        // This is enough for each major version!
        // The epoch should be bumped for each major version.

        var utcTimestamp = timestamp.ToUniversalTime();
        var timeSinceEpoch = utcTimestamp - epoch;

        var intervalsSinceEpoch = Convert.ToInt32(timeSinceEpoch.TotalSeconds / 30.0);
        if (intervalsSinceEpoch > 16_777_215)
        {
            throw new ArgumentOutOfRangeException(nameof(timestamp));
        }

        var sixteenBitLSB = (ushort)(intervalsSinceEpoch & 0xFFFF);
        var eightBitMSB = (byte)((intervalsSinceEpoch >> 16) & 0xFF);

        return new Version(
            major: major,
            minor: eightBitMSB,
            build: sixteenBitLSB);
    }
}
