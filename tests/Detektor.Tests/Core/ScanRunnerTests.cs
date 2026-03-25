using Detektor.Core.Pipeline;
using Detektor.Core.Services;

namespace Detektor.Tests.Core;

public sealed class ScanRunnerTests
{
    [Fact]
    public async Task RunAsync_ReturnsFailure_WhenPipelineIsNotImplemented_ForValidTarget()
    {
        var scanRunner = new ScanRunner();

        var result = await scanRunner.RunAsync(new ScanRequest("."));

        Assert.Equal(1, result.ExitCode);
        Assert.Contains(result.Messages, message => message.StartsWith("target path resolved:", StringComparison.Ordinal));
        Assert.Contains("scan pipeline initialized", result.Messages);
        Assert.Contains("artifact loading not implemented yet", result.Messages);
    }

    [Fact]
    public async Task RunAsync_ReturnsFailure_WhenTargetDoesNotExist()
    {
        var scanRunner = new ScanRunner();
        var missingTarget = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}-missing");

        var result = await scanRunner.RunAsync(new ScanRequest(missingTarget));

        Assert.Equal(1, result.ExitCode);
        Assert.Contains(result.Messages, message => message.StartsWith("target path resolved:", StringComparison.Ordinal));
        Assert.Contains("target path does not exist", result.Messages);
    }
}

