using Detektor.Artifacts;
using Detektor.Core.Pipeline;

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

        ArgumentException.ThrowIfNullOrWhiteSpace(request.TargetPath);

        var resolvedTargetPath = Path.GetFullPath(request.TargetPath);

        if (!File.Exists(resolvedTargetPath) && !Directory.Exists(resolvedTargetPath))
        {
            var failureMessages = new[]
            {
                "Detektor CLI started",
                $"target received: {request.TargetPath}",
                $"target path resolved: {resolvedTargetPath}",
                "target path does not exist"
            };

            return new ScanResult(1, failureMessages);
        }

        var artifacts = await _artifactLoader.LoadAsync(resolvedTargetPath, cancellationToken);

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

        return new ScanResult(1, messages);
    }
}
