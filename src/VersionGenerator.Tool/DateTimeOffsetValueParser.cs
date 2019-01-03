using LibGit2Sharp;
using McMaster.Extensions.CommandLineUtils.Abstractions;
using System;
using System.Globalization;
using System.IO;

namespace VersionGenerator.Tool
{
    internal sealed class DateTimeOffsetValueParser : IValueParser
    {
        public Type TargetType { get; } = typeof(DateTimeOffset);

        public object Parse(string argName, string value, CultureInfo culture)
        {
            // If the user entered "now", we'll just return the current system time.
            if (String.Equals(value, "now", StringComparison.OrdinalIgnoreCase))
            {
                return DateTimeOffset.Now;
            }

            // If the user entered a string starting with "git:", we'll check if there
            // is a git repository at that path, and if there is, use the commit timestamp.
            if (value?.StartsWith("git:") ?? false)
            {
                var path = value.Substring(4, value.Length - 4);
                if (Directory.Exists(path))
                {
                    try
                    {
                        var repository = new Repository(path);
                        return repository.Head.Tip.Author.When;
                    }
                    catch (RepositoryNotFoundException ex)
                    {
                        throw new FormatException($"Invalid value specified for {argName}. {ex.Message}");
                    }
                }
                else
                {
                    throw new FormatException($"Invalid value specified for {argName}. Path '{path}' does not point at a valid directory.");
                }
            }

            // Finally, we'll just try to parse the value as normal.
            if (DateTimeOffset.TryParse(value, culture.DateTimeFormat, DateTimeStyles.RoundtripKind | DateTimeStyles.AllowWhiteSpaces, out var timestamp))
            {
                return timestamp;
            }

            throw new FormatException($"Invalid value specified for {argName}. '{value}' can't be parsed as a valid date-time (with offset).");
        }
    }
}
