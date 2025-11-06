using System;

namespace VersionGenerator;

/// <summary>
/// Provides functionality to generate type-C versions.
/// </summary>
public static class VersionTypeC
{
    /// <summary>
    /// Generates a new type-C version.
    /// </summary>
    /// <param name="timestamp">The timestamp to be encoded in the version number.</param>
    /// <param name="major">The major version.</param>
    /// <returns>A type-C version.</returns>
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
