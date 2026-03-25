using Detektor.Core.Pipeline;
using Detektor.Core.Services;
using System.CommandLine;

namespace Detektor.Cli.Commands;

internal static class ScanCommand
{
    public static Command Create(ScanRunner scanRunner)
    {
        var targetArgument = new Argument<string>("target")
        {
            Description = "The file system target to scan."
        };

        var command = new Command("scan", "Run the Detektor scan pipeline against a target.")
        {
            targetArgument
        };

        command.SetAction(async (parseResult, cancellationToken) =>
        {
            var request = new ScanRequest(parseResult.GetRequiredValue(targetArgument));
            var result = await scanRunner.RunAsync(request, cancellationToken);

            foreach (var line in result.Messages)
            {
                Console.WriteLine(line);
            }

            return result.ExitCode;
        });

        return command;
    }
}
