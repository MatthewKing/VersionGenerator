using System.CommandLine;
using VersionGenerator.Tool;

var majorOption = Options.MajorVersion();
var minorOption = Options.MinorVersion();
var timestampSpecifierOption = Options.Timestamp();

var root = new RootCommand();
root.Name = "vgen";
root.Description = "Version Generator";
root.AddCommand(Commands.A(majorOption, minorOption, timestampSpecifierOption));
root.AddCommand(Commands.B(timestampSpecifierOption));
root.AddCommand(Commands.C(majorOption, timestampSpecifierOption));

await root.InvokeAsync(args);
