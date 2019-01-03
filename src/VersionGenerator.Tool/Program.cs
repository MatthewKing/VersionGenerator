using McMaster.Extensions.CommandLineUtils;
using System;
using System.Reflection;

namespace VersionGenerator.Tool
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            var now = DateTimeOffset.Now;

            var app = new CommandLineApplication();
            app.Name = "VersionGenerator.exe";
            app.FullName = "Version Generator";
            app.ValueParsers.AddOrReplace(new DateTimeOffsetValueParser());

            app.HelpOption();
            app.VersionOption("-v|--version", ThisAssembly.GetName().Version.ToString());

            app.Command("A", command =>
            {
                command.Description = "Generate a type-A version";

                command.HelpOption();

                var optionTimestamp = command.OptionTimestamp();
                var optionMajor = command.OptionMajorVersion();
                var optionMinor = command.OptionMinorVersion();

                command.OnExecute(() =>
                {
                    var version = VersionTypeA.GenerateFromTimestamp(
                        timestamp: optionTimestamp.ParsedValue ?? now,
                        major: optionMajor.ParsedValue ?? 1,
                        minor: optionMinor.ParsedValue ?? 0);

                    Console.WriteLine(version);

                    return 0;
                });
            });

            app.Command("B", command =>
            {
                command.Description = "Generate a type-B version";

                command.HelpOption();

                var optionTimestamp = command.OptionTimestamp();
                var optionMajor = command.OptionMajorVersion();
                var optionMinor = command.OptionMinorVersion();

                command.OnExecute(() =>
                {
                    var version = VersionTypeB.GenerateFromTimestamp(
                        timestamp: optionTimestamp.ParsedValue ?? now);

                    Console.WriteLine(version);

                    return 0;
                });
            });

            app.Command("C", command =>
            {
                command.Description = "Generate a type-C version";

                command.HelpOption();

                var optionTimestamp = command.OptionTimestamp();
                var optionMajor = command.OptionMajorVersion();

                command.OnExecute(() =>
                {
                    var version = VersionTypeC.GenerateFromTimestamp(
                        timestamp: optionTimestamp.ParsedValue ?? now,
                        major: optionMajor.ParsedValue ?? 1);

                    Console.WriteLine(version);

                    return 0;
                });
            });

            // When executing without a command, just show the help.
            app.OnExecute(() =>
            {
                app.ShowHelp();
            });

            // Run the app.
            try
            {
                return app.Execute(args);
            }
            catch (Exception)
            {
                app.ShowHelp();
                return -1;
            }
        }

        /// <summary>
        /// Gets this assembly.
        /// </summary>
        private static Assembly ThisAssembly { get; } = typeof(Program).GetTypeInfo().Assembly;
    }
}
