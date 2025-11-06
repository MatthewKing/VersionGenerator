using System;

namespace VersionGenerator;

/// <summary>
/// Provides functionality to generate type-B versions.
/// </summary>
public static class VersionTypeB
{
    /// <summary>
    /// Generates a new type-B version.
    /// </summary>
    /// <param name="timestamp">The timestamp to be encoded in the version number.</param>
    /// <returns>A type-B version.</returns>
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
