using McMaster.Extensions.CommandLineUtils;
using System;

namespace VersionGenerator.Tool
{
    internal static class CommandOptionExtensions
    {
        public static CommandOption<int?> OptionMajorVersion(this CommandLineApplication app, int defaultValue = 1)
        {
            return app.Option<int?>("-x|--major <VERSION>", "Major version X => (X.y.z)", CommandOptionType.SingleValue);
        }

        public static CommandOption<int?> OptionMinorVersion(this CommandLineApplication app)
        {
            return app.Option<int?>("-y|--minor <VERSION>", "Minor version Y => (x.Y.z)", CommandOptionType.SingleValue);
        }

        public static CommandOption<DateTimeOffset?> OptionTimestamp(this CommandLineApplication app)
        {
            return app.Option<DateTimeOffset?>("-t|--timestamp <TIMESTAMP>", "Timestamp (\"now\", \"yyyy-MM-ddTHH:mm:ssZ\", or \"git:<PATH>\")", CommandOptionType.SingleValue);
        }
    }
}
