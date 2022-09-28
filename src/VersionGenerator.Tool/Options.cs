using System;
using System.CommandLine;
using System.Linq;

namespace VersionGenerator.Tool;

internal static class Options
{
    public static Option<int> MajorVersion()
    {
        return new Option<int>(
            aliases: new[] { "--major", "-x" },
            description: "Major version X => (X.y.z)",
            getDefaultValue: () => 1);
    }

    public static Option<int> MinorVersion()
    {
        return new Option<int>(
            aliases: new[] { "--minor", "-y" },
            description: "Minor version Y => (x.Y.z)",
            getDefaultValue: () => 0);
    }

    public static Option<DateTimeOffset> Timestamp()
    {
        var option = new Option<DateTimeOffset>(
            aliases: new[] { "--timestamp", "-t" },
            description: "Timestamp (\"now\", or \"git:<PATH>\", or \"yyyy-MM-ddTHH:mm:ssZ\")",
            parseArgument: result =>
            {
                if (TimestampParser.TryParse(result.Tokens.Single().Value, out var timestamp, out var errorMessage))
                {
                    return timestamp;
                }
                else
                {
                    result.ErrorMessage = string.Join(" ", "Invalid timestamp.", errorMessage);
                    return DateTimeOffset.UtcNow;
                }
            });

        option.AddValidator(result =>
        {
            if (result.GetValueForOption(option).Year < 2000)
            {
                result.ErrorMessage = "This version format is only valid for dates in the year 2000 and later.";
            }
        });

        return option;
    }
}
