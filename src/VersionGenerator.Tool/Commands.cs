using System;
using System.CommandLine;

namespace VersionGenerator.Tool;

public static class Commands
{
    public static Command A(Option<int> majorOption, Option<int> minorOption, Option<string> timestampSpecifierOption)
    {
        var command = new Command("A") { majorOption, minorOption, timestampSpecifierOption };
        command.Description = "Generates a version using the type-A format";
        command.SetHandler((int major, int minor, string timestampSpecifier) =>
        {
            if (TimestampParser.TryParse(timestampSpecifier, out var timestamp))
            {
                var version = VersionTypeA.GenerateFromTimestamp(timestamp, major, minor);
                Console.WriteLine(version);
            }
        }, majorOption, minorOption, timestampSpecifierOption);

        return command;
    }

    public static Command B(Option<string> timestampSpecifierOption)
    {
        var command = new Command("B") { timestampSpecifierOption };
        command.Description = "Generates a version using the type-B format";
        command.SetHandler((string timestampSpecifier) =>
        {
            if (TimestampParser.TryParse(timestampSpecifier, out var timestamp))
            {
                var version = VersionTypeB.GenerateFromTimestamp(timestamp);
                Console.WriteLine(version);
            }
        }, timestampSpecifierOption);

        return command;
    }

    public static Command C(Option<int> majorOption, Option<string> timestampSpecifierOption)
    {
        var command = new Command("C") { majorOption, timestampSpecifierOption };
        command.Description = "Generates a version using the type-C format";
        command.SetHandler((int major, string timestampSpecifier) =>
        {
            if (TimestampParser.TryParse(timestampSpecifier, out var timestamp))
            {
                var version = VersionTypeC.GenerateFromTimestamp(timestamp, major);
                Console.WriteLine(version);
            }
        }, majorOption, timestampSpecifierOption);

        return command;
    }
}
