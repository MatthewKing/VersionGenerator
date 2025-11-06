using System;

namespace VersionGenerator;

/// <summary>
/// Provides functionality to generate type-D versions.
/// </summary>
public static class VersionTypeD
{
    /// <summary>
    /// Generates a new type-D version.
    /// This is very similar to a type-C version, but uses 10-minute increments instead of 60-minute increments.
    /// </summary>
    /// <param name="timestamp">The timestamp to be encoded in the version number.</param>
    /// <param name="major">The major version.</param>
    /// <returns>A type-D version.</returns>
    public static Version GenerateFromTimestamp(DateTimeOffset timestamp, int major)
    {
        var utcTimestamp = timestamp.ToUniversalTime();
        var startOfYear = new DateTimeOffset(utcTimestamp.Year, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var increment = (utcTimestamp - startOfYear).TotalMinutes / 10;

        return new Version(
            major: major,
            minor: utcTimestamp.Year - 2000,
            build: 10000 + Convert.ToUInt16(increment));
    }
}
