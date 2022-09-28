using System.CommandLine;

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

    public static Option<string> Timestamp()
    {
        return new Option<string>(
            aliases: new[] { "--timestamp", "-t" },
            description: "Timestamp (\"now\", or \"git:<PATH>\", or \"yyyy-MM-ddTHH:mm:ssZ\")",
            getDefaultValue: () => "now");
    }
}
