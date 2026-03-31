using Detektor.Artifacts;
using Detektor.Core.Pipeline;
using System.Text.Json;
using YamlDotNet.Core;

namespace Detektor.Core.Services;

public sealed class ScanRunner
{
    private readonly ArtifactLoader _artifactLoader;

    public ScanRunner()
        : this(new ArtifactLoader())
    {
    }

    public ScanRunner(ArtifactLoader artifactLoader)
    {
        _artifactLoader = artifactLoader ?? throw new ArgumentNullException(nameof(artifactLoader));
    }

    public Task<ScanResult> RunAsync(ScanRequest request, CancellationToken cancellationToken = default)
        => RunInternalAsync(request, cancellationToken);

    private async Task<ScanResult> RunInternalAsync(ScanRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        ArgumentException.ThrowIfNullOrWhiteSpace(request.TargetPath);

        var resolvedTargetPath = Path.GetFullPath(request.TargetPath);

        if (!File.Exists(resolvedTargetPath) && !Directory.Exists(resolvedTargetPath))
        {
            return CreateFailureResult(
                resolvedTargetPath,
                "target path does not exist",
                request.TargetPath);
        }

        IReadOnlyList<Artifact> artifacts;

        try
        {
            artifacts = await _artifactLoader.LoadAsync(resolvedTargetPath, cancellationToken);
        }
        catch (Exception exception) when (IsExpectedArtifactLoadException(exception))
        {
            return CreateFailureResult(
                resolvedTargetPath,
                "artifact loading failed",
                request.TargetPath);
        }

        var messages = new[]
        {
            "Detektor CLI started",
            $"target received: {request.TargetPath}",
            $"target path resolved: {resolvedTargetPath}",
            "scan pipeline initialized",
            $"artifact loading completed: {artifacts.Count} artifact(s) discovered",
            "rules not implemented yet",
            "reporting not implemented yet"
        };

        return new ScanResult(1, resolvedTargetPath, artifacts.Count, messages);
    }

    private static ScanResult CreateFailureResult(string resolvedTargetPath, string failureMessage, string originalTargetPath)
    {
        var messages = new[]
        {
            "Detektor CLI started",
            $"target received: {originalTargetPath}",
            $"target path resolved: {resolvedTargetPath}",
            failureMessage
        };

        return new ScanResult(1, resolvedTargetPath, 0, messages);
    }

    private static bool IsExpectedArtifactLoadException(Exception exception)
        => exception is IOException
            or UnauthorizedAccessException
            or PathTooLongException
            or NotSupportedException
            or JsonException
            or YamlException;
}
