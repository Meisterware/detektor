using Detektor.Core.Pipeline;

namespace Detektor.Core.Services;

public sealed class ScanRunner
{
    public Task<ScanResult> RunAsync(ScanRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        ArgumentException.ThrowIfNullOrWhiteSpace(request.TargetPath);

        var resolvedTargetPath = Path.GetFullPath(request.TargetPath);
        var messages = new[]
        {
            "Detektor CLI started",
            $"target received: {request.TargetPath}",
            $"target path resolved: {resolvedTargetPath}",
            "scan pipeline initialized",
            "artifact loading not implemented yet",
            "rules not implemented yet",
            "reporting not implemented yet"
        };

        return Task.FromResult(new ScanResult(0, messages));
    }
}
