using Detektor.Cli.Commands;
using Detektor.Core.Services;
using System.CommandLine;

namespace Detektor.Cli;

internal static class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            var scanRunner = new ScanRunner();

            var rootCommand = new RootCommand("Detektor is a minimal, deterministic CLI scanner for AI agent security.")
            {
                ScanCommand.Create(scanRunner)
            };

            return await rootCommand.Parse(args).InvokeAsync();
        }
        catch (Exception exception) when (IsRecoverableException(exception))
        {
            Console.Error.WriteLine("Detektor CLI failed unexpectedly.");
            return 1;
        }
    }

    private static bool IsRecoverableException(Exception exception)
        => exception is not OutOfMemoryException
            and not StackOverflowException
            and not AccessViolationException
            and not AppDomainUnloadedException
            and not BadImageFormatException
            and not InvalidProgramException;
}
