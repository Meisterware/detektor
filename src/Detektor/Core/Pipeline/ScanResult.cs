namespace Detektor.Core.Pipeline;

public sealed record ScanResult(
    int ExitCode,
    string? ResolvedTargetPath,
    int ArtifactCount,
    IReadOnlyList<string> Messages);
