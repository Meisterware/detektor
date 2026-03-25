namespace Detektor.Core.Pipeline;

public sealed record ScanResult(int ExitCode, IReadOnlyList<string> Messages);
