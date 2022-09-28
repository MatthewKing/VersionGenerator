using System;
using System.Diagnostics;
using System.IO;

namespace VersionGenerator.Tool;

internal static class TimestampParser
{
    public static bool TryParse(string value, out DateTimeOffset timestamp)
    {
        // Handle missing input.
        if (string.IsNullOrWhiteSpace(value))
        {
            timestamp = default;
            return false;
        }

        // If the user entered "now", we'll just return the current system time.
        if (string.Equals(value, "now", StringComparison.OrdinalIgnoreCase))
        {
            timestamp = DateTimeOffset.UtcNow;
            return true;
        }

        // If the user entered a string starting with "git:", we'll check if there
        // is a git repository at that path, and if there is, use the commit timestamp.
        if (value.StartsWith("git:"))
        {
            var path = value.Substring(4, value.Length - 4);

            // Invalid path.
            if (!Directory.Exists(path))
            {
                timestamp = default;
                return false;
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

                // Invalid exit code.
                if (process.ExitCode != 0)
                {
                    timestamp = default;
                    return false;
                }

                var output = process.StandardOutput.ReadToEnd();
                if (Int64.TryParse(output, out var secondsSinceEpoch))
                {
                    timestamp = DateTimeOffset.FromUnixTimeSeconds(secondsSinceEpoch);
                    return true;
                }
                else
                {
                    // ToDo: Handle input that can't be parsed.
                }
            }
            catch (Exception ex)
            {
                // ToDo: Handle exeptions?
            }
        }

        // Finally, we'll just try to parse the value as normal.
        if (DateTimeOffset.TryParse(value, out var parsed))
        {
            timestamp = parsed;
            return true;
        }

        timestamp = default;
        return false;
    }
}
