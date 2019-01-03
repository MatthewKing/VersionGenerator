using McMaster.Extensions.CommandLineUtils.Abstractions;
using System;
using System.Diagnostics;
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
                if (!Directory.Exists(path))
                {
                    throw new FormatException($"Invalid value specified for {argName}. Path '{path}' does not point at a valid directory.");
                }

                try
                {
                    var psi = new ProcessStartInfo("git", "log -1 --format=%ct");
                    psi.WorkingDirectory = path;
                    psi.RedirectStandardInput = true;
                    psi.RedirectStandardOutput = true;
                    psi.RedirectStandardError = true;
                    psi.UseShellExecute = false;

                    var process = Process.Start(psi);

                    process.WaitForExit(10_000); // Time out after 10 seconds.

                    if (process.ExitCode == 0)
                    {
                        var output = process.StandardOutput.ReadToEnd();
                        if (Int64.TryParse(output, out var secondsSinceEpoch))
                        {
                            return DateTimeOffset.FromUnixTimeSeconds(secondsSinceEpoch);
                        }
                    }

                    throw new FormatException($"Invalid value specified for {argName}. Could not calculate timestamp from git repository.");
                }
                catch (Exception ex)
                {
                    throw new FormatException($"Invalid value specified for {argName}. {ex.Message}");
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
