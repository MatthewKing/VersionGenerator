using System;

namespace VersionGenerator;

/// <summary>
/// Provides functionality to generate type-A versions.
/// </summary>
public static class VersionTypeA
{
    /// <summary>
    /// Gets the type-A version epoch.
    /// </summary>
    private static DateTimeOffset Epoch { get; } = new DateTimeOffset(2000, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

    /// <summary>
    /// Generates a new type-A version.
    /// </summary>
    /// <param name="timestamp">The timestamp to be encoded in the version number.</param>
    /// <param name="major">The major version.</param>
    /// <param name="minor">The minor version.</param>
    /// <returns>A type-A version.</returns>
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
