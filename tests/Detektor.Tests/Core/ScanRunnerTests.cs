using Detektor.Core.Pipeline;
using Detektor.Core.Services;

namespace Detektor.Tests.Core;

public sealed class ScanRunnerTests
{
    [Fact]
    public async Task RunAsync_ReturnsDeterministicFailure_ForValidTarget()
    {
        var scanRunner = new ScanRunner();

        var result = await scanRunner.RunAsync(new ScanRequest("."));

        Assert.Equal(1, result.ExitCode);
        Assert.Equal(Path.GetFullPath("."), result.ResolvedTargetPath);
        Assert.True(result.ArtifactCount >= 0);
        Assert.Contains(result.Messages, message => message.StartsWith("target path resolved:", StringComparison.Ordinal));
        Assert.Contains("scan pipeline initialized", result.Messages);
        Assert.Contains(result.Messages, message => message.StartsWith("artifact loading completed:", StringComparison.Ordinal));
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
        var invalidJsonPath = Path.Combine(targetDirectory.FullName, "agent.json");

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
}
