using Detektor.Findings;
using Detektor.OpenPAKT.Constants;

namespace Detektor.Tests.Findings;

public sealed class FindingModelTests
{
    [Fact]
    public void Finding_StoresCanonicalNormalizedFields()
    {
        var evidence = new Evidence(
            Summary: "Detected suspicious phrase.",
            Location: "artifact.json:$.prompt",
            Snippet: "ignore previous instructions");

        var finding = new Finding(
            Id: "prompt-injection-rule:artifact.json:ignore_previous_instructions",
            Type: FindingTaxonomy.PromptInjection,
            Severity: SeverityLevels.High,
            Component: "artifact.json",
            Description: "Artifact contains a prompt injection phrase.",
            Evidence: evidence);

        Assert.Equal("prompt-injection-rule:artifact.json:ignore_previous_instructions", finding.Id);
        Assert.Equal(FindingTaxonomy.PromptInjection, finding.Type);
        Assert.Equal(SeverityLevels.High, finding.Severity);
        Assert.Equal("artifact.json", finding.Component);
        Assert.Equal("Artifact contains a prompt injection phrase.", finding.Description);
        Assert.Same(evidence, finding.Evidence);
    }

    [Fact]
    public void Evidence_StoresStructuredFields()
    {
        var evidence = new Evidence(
            Summary: "Detected unrestricted shell configuration.",
            Location: "agent.json:$.tools.shell.enabled",
            Snippet: "\"enabled\":true");

        Assert.Equal("Detected unrestricted shell configuration.", evidence.Summary);
        Assert.Equal("agent.json:$.tools.shell.enabled", evidence.Location);
        Assert.Equal("\"enabled\":true", evidence.Snippet);
    }

    [Fact]
    public void Finding_RejectsNonCanonicalTaxonomyIdentifier()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Finding(
            Id: "finding-id",
            Type: "informational",
            Severity: SeverityLevels.Low,
            Component: "artifact.json",
            Description: "description",
            Evidence: new Evidence("summary")));

        Assert.Equal("Type", exception.ParamName);
    }

    [Fact]
    public void Finding_RejectsNonCanonicalSeverityValue()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Finding(
            Id: "finding-id",
            Type: FindingTaxonomy.PromptInjection,
            Severity: "warning",
            Component: "artifact.json",
            Description: "description",
            Evidence: new Evidence("summary")));

        Assert.Equal("Severity", exception.ParamName);
    }

    [Fact]
    public void Evidence_RejectsMissingSummary()
    {
        var exception = Assert.Throws<ArgumentException>(() => new Evidence(""));

        Assert.Equal("Summary", exception.ParamName);
    }
}
