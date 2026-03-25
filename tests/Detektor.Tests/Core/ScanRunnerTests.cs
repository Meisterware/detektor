using Detektor.Core.Pipeline;
using Detektor.Core.Services;

namespace Detektor.Tests.Core;

public sealed class ScanRunnerTests
{
    [Fact]
    public async Task RunAsync_ReturnsPlaceholderResult_ForValidTarget()
    {
        var scanRunner = new ScanRunner();

        var result = await scanRunner.RunAsync(new ScanRequest("."));

        Assert.Equal(0, result.ExitCode);
        Assert.Contains(result.Messages, message => message.StartsWith("target path resolved:", StringComparison.Ordinal));
        Assert.Contains("artifact loading not implemented yet", result.Messages);
    }
}
