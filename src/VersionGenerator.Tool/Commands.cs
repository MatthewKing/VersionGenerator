using System;
using System.CommandLine;

namespace VersionGenerator.Tool;

public static class Commands
{
    public static Command A(Option<int> majorOption, Option<int> minorOption, Option<DateTimeOffset> timestampOption)
    {
        var command = new Command("A") { majorOption, minorOption, timestampOption };
        command.Description = "Generates a version using the type-A format";
        command.SetHandler((int major, int minor, DateTimeOffset timestamp) =>
        {
            var version = VersionTypeA.GenerateFromTimestamp(timestamp, major, minor);
            Console.WriteLine(version);
        }, majorOption, minorOption, timestampOption);

        return command;
    }

    public static Command B(Option<DateTimeOffset> timestampOption)
    {
        var command = new Command("B") { timestampOption };
        command.Description = "Generates a version using the type-B format";
        command.SetHandler((DateTimeOffset timestamp) =>
        {
            var version = VersionTypeB.GenerateFromTimestamp(timestamp);
            Console.WriteLine(version);
        }, timestampOption);

        return command;
    }

    public static Command C(Option<int> majorOption, Option<DateTimeOffset> timestampOption)
    {
        var command = new Command("C") { majorOption, timestampOption };
        command.Description = "Generates a version using the type-C format";
        command.SetHandler((int major, DateTimeOffset timestamp) =>
        {
            var version = VersionTypeC.GenerateFromTimestamp(timestamp, major);
            Console.WriteLine(version);
        }, majorOption, timestampOption);

        return command;
    }

    public static Command D(Option<int> majorOption, Option<DateTimeOffset> timestampOption)
    {
        var command = new Command("D") { majorOption, timestampOption };
        command.Description = "Generates a version using the type-D format";
        command.SetHandler((int major, DateTimeOffset timestamp) =>
        {
            var version = VersionTypeD.GenerateFromTimestamp(timestamp, major);
            Console.WriteLine(version);
        }, majorOption, timestampOption);

        return command;
    }

    public static Command E(Option<int> majorOption, Option<DateTimeOffset> epochOption, Option<DateTimeOffset> timestampOption)
    {
        var command = new Command("E") { majorOption, epochOption, timestampOption };
        command.Description = "Generates a version using the type-D format";
        command.SetHandler((int major, DateTimeOffset epoch, DateTimeOffset timestamp) =>
        {
            var version = VersionTypeE.GenerateFromTimestamp(timestamp, major, epoch);
            Console.WriteLine(version);
        }, majorOption, epochOption, timestampOption);

        return command;
    }
}
