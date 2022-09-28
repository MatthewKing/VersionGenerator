using System;
using System.Diagnostics;
using System.IO;

namespace VersionGenerator.Tool;

internal static class TimestampParser
{
    public static bool TryParse(string value, out DateTimeOffset timestamp, out string message)
    {
        // Handle missing input.
        if (string.IsNullOrWhiteSpace(value))
        {
            timestamp = default;
            message = "No timestamp value specified.";
            return false;
        }

        // If the user entered "now", we'll just return the current system time.
        if (string.Equals(value, "now", StringComparison.OrdinalIgnoreCase))
        {
            timestamp = DateTimeOffset.UtcNow;
            message = null;
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
                message = $"Directory '{path}' does not exist.";
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
                    message = $"Git exited unsuccessfully with exit code {process.ExitCode}.";
                    return false;
                }

                var output = process.StandardOutput.ReadToEnd();
                if (Int64.TryParse(output, out var secondsSinceEpoch))
                {
                    timestamp = DateTimeOffset.FromUnixTimeSeconds(secondsSinceEpoch);
                    message = null;
                    return true;
                }
                else
                {
                    timestamp = default;
                    message = $"Value '{output}' cannot be parsed as a unix timestamp.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                timestamp = default;
                message = ex.Message;
                return false;
            }
        }

        // Finally, we'll just try to parse the value as normal.
        if (DateTimeOffset.TryParse(value, out var parsed))
        {
            timestamp = parsed;
            message = null;
            return true;
        }
        else
        {
            timestamp = default;
            message = $"Value '{value}' cannot be parsed as a timestamp.";
            return false;
        }
    }
}
