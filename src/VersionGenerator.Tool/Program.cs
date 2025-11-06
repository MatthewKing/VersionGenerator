using System.CommandLine;
using VersionGenerator.Tool;

var majorOption = Options.MajorVersion();
var minorOption = Options.MinorVersion();
var epochOption = Options.Epoch();
var timestampOption = Options.Timestamp();

var root = new RootCommand();
root.Name = "vgen";
root.Description = "Version Generator";
root.AddCommand(Commands.A(majorOption, minorOption, timestampOption));
root.AddCommand(Commands.B(timestampOption));
root.AddCommand(Commands.C(majorOption, timestampOption));
root.AddCommand(Commands.D(majorOption, timestampOption));
root.AddCommand(Commands.E(majorOption, epochOption, timestampOption));

await root.InvokeAsync(args);
