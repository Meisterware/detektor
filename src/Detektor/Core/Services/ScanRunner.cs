using Detektor.Artifacts;
using Detektor.Core.Pipeline;
using Detektor.Findings;
using Detektor.Rules;
using System.Text.Json;
using YamlDotNet.Core;

namespace Detektor.Core.Services;

public sealed class ScanRunner
{
    private readonly ArtifactLoader _artifactLoader;
    private readonly RuleEngine _ruleEngine;

    public ScanRunner()
        : this(
            new ArtifactLoader(),
            new RuleEngine(
            [
                new PromptInjectionRule(),
                new ToolPermissionValidationRule()
            ]))
    {
    }

    public ScanRunner(ArtifactLoader artifactLoader, RuleEngine ruleEngine)
    {
        _artifactLoader = artifactLoader ?? throw new ArgumentNullException(nameof(artifactLoader));
        _ruleEngine = ruleEngine ?? throw new ArgumentNullException(nameof(ruleEngine));
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

        var findings = _ruleEngine.Evaluate(artifacts);

        var messages = new[]
        {
            "Detektor CLI started",
            $"target received: {request.TargetPath}",
            $"target path resolved: {resolvedTargetPath}",
            "scan pipeline initialized",
            $"artifact loading completed: {artifacts.Count} artifact(s) discovered",
            $"rule evaluation completed: {findings.Count} finding(s) produced",
            "reporting not implemented yet"
        };

        return new ScanResult(0, resolvedTargetPath, artifacts.Count, messages, findings);
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

        return new ScanResult(1, resolvedTargetPath, 0, messages, Array.Empty<Finding>());
    }

    private static bool IsExpectedArtifactLoadException(Exception exception)
        => exception is IOException
            or UnauthorizedAccessException
            or PathTooLongException
            or NotSupportedException
            or JsonException
            or YamlException;
}
