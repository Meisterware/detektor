using Detektor.Core.Pipeline;
using Detektor.Core.Services;

namespace Detektor.Tests.Core;

public sealed class ScanRunnerTests
{
    [Fact]
    public async Task RunAsync_ReturnsDeterministicFailure_ForValidTarget()
    {
        var scanRunner = new ScanRunner();
        var targetDirectory = Directory.CreateTempSubdirectory();
        var jsonPath = Path.Join(targetDirectory.FullName, "agent.json");
        var yamlPath = Path.Join(targetDirectory.FullName, "prompt.yaml");

        try
        {
            await File.WriteAllTextAsync(jsonPath, """
{
  "name": "agent"
}
""");
            await File.WriteAllTextAsync(yamlPath, """
messages:
  - role: system
    content: stay safe
""");

            var result = await scanRunner.RunAsync(new ScanRequest(targetDirectory.FullName));

            Assert.Equal(1, result.ExitCode);
            Assert.Equal(Path.GetFullPath(targetDirectory.FullName), result.ResolvedTargetPath);
            Assert.Equal(2, result.ArtifactCount);
            Assert.Equal(
                [
                    "Detektor CLI started",
                    $"target received: {targetDirectory.FullName}",
                    $"target path resolved: {Path.GetFullPath(targetDirectory.FullName)}",
                    "scan pipeline initialized",
                    "artifact loading completed: 2 artifact(s) discovered",
                    "rules not implemented yet",
                    "reporting not implemented yet"
                ],
                result.Messages);
        }
        finally
        {
            targetDirectory.Delete(recursive: true);
        }
    }

    [Fact]
    public async Task RunAsync_ReturnsFailure_WhenTargetDoesNotExist()
    {
        var scanRunner = new ScanRunner();
        var missingTarget = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}-missing");

        var result = await scanRunner.RunAsync(new ScanRequest(missingTarget));

        Assert.Equal(1, result.ExitCode);
        Assert.Equal(Path.GetFullPath(missingTarget), result.ResolvedTargetPath);
        Assert.Equal(0, result.ArtifactCount);
        Assert.Contains(result.Messages, message => message.StartsWith("target path resolved:", StringComparison.Ordinal));
        Assert.Contains("target path does not exist", result.Messages);
    }

    [Fact]
    public async Task RunAsync_ReturnsDeterministicFailure_WhenArtifactLoadingFails()
    {
        var scanRunner = new ScanRunner();
        var targetDirectory = Directory.CreateTempSubdirectory();
        var invalidJsonPath = Path.Join(targetDirectory.FullName, "agent.json");

        try
        {
            await File.WriteAllTextAsync(invalidJsonPath, "{ invalid json");

            var result = await scanRunner.RunAsync(new ScanRequest(targetDirectory.FullName));

            Assert.Equal(1, result.ExitCode);
            Assert.Equal(Path.GetFullPath(targetDirectory.FullName), result.ResolvedTargetPath);
            Assert.Equal(0, result.ArtifactCount);
            Assert.Contains("artifact loading failed", result.Messages);
        }
        finally
        {
            targetDirectory.Delete(recursive: true);
        }
    }

    [Fact]
    public async Task RunAsync_ReturnsDeterministicFailure_WhenYamlParsingFails()
    {
        var scanRunner = new ScanRunner();
        var targetDirectory = Directory.CreateTempSubdirectory();
        var invalidYamlPath = Path.Join(targetDirectory.FullName, "prompt.yaml");

        try
        {
            await File.WriteAllTextAsync(invalidYamlPath, "key: [unterminated");

            var result = await scanRunner.RunAsync(new ScanRequest(targetDirectory.FullName));

            Assert.Equal(1, result.ExitCode);
            Assert.Equal(Path.GetFullPath(targetDirectory.FullName), result.ResolvedTargetPath);
            Assert.Equal(0, result.ArtifactCount);
            Assert.Contains("artifact loading failed", result.Messages);
        }
        finally
        {
            targetDirectory.Delete(recursive: true);
        }
    }
}
