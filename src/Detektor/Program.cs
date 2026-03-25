using Detektor.Cli.Commands;
using Detektor.Core.Services;
using System.CommandLine;

namespace Detektor.Cli;

internal static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var scanRunner = new ScanRunner();

        var rootCommand = new RootCommand("Detektor is a minimal, deterministic CLI scanner for AI agent security.")
        {
            ScanCommand.Create(scanRunner)
        };

        return await rootCommand.Parse(args).InvokeAsync();
    }
}
