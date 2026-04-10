using System.Text.Json.Nodes;
using Detektor.Artifacts;
using Detektor.Rules;

namespace Detektor.Tests.Rules;

public sealed class ToolPermissionValidationRuleTests
{
    private readonly ToolPermissionValidationRule _rule = new();

    [Fact]
    public void Evaluate_DetectsUnrestrictedShellConfig()
    {
        var artifact = CreateArtifact("""
{
  "tools": {
    "shell": {
      "enabled": true
    }
  }
}
""");

        var finding = Assert.Single(_rule.Evaluate(artifact));

        Assert.Equal("tool_abuse_privilege_escalation", finding.Type);
        Assert.Equal("high", finding.Severity);
        Assert.False(string.IsNullOrWhiteSpace(finding.Evidence.Summary));
    }

    [Fact]
    public void Evaluate_DetectsEmptyAllowlistPattern()
    {
        var artifact = CreateArtifact("""
{
  "tools": {
    "shell": {
      "enabled": true,
      "allowlist": []
    }
  }
}
""");

        var finding = Assert.Single(_rule.Evaluate(artifact));

        Assert.Equal("tool_abuse_privilege_escalation", finding.Type);
        Assert.Equal("critical", finding.Severity);
    }

    [Fact]
    public void Evaluate_IgnoresSafeAllowlistedConfig()
    {
        var artifact = CreateArtifact("""
{
  "tools": {
    "shell": {
      "enabled": true,
      "allowlist": ["echo", "pwd"]
    }
  }
}
""");

        var findings = _rule.Evaluate(artifact);

        Assert.Empty(findings);
    }

    private static Artifact CreateArtifact(string json)
        => new("artifact.json", "generic", JsonNode.Parse(json), "json");
}
