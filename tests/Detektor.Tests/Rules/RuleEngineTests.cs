using System.Text.Json.Nodes;
using Detektor.Artifacts;
using Detektor.Findings;
using Detektor.OpenPAKT.Constants;
using Detektor.Rules;

namespace Detektor.Tests.Rules;

public sealed class RuleEngineTests
{
    [Fact]
    public void Evaluate_ExecutesMultipleRulesAgainstMultipleArtifacts()
    {
        Artifact[] artifacts =
        [
            new Artifact("b.json", "generic", JsonNode.Parse("""{"value":"b"}"""), "json"),
            new Artifact("a.json", "generic", JsonNode.Parse("""{"value":"a"}"""), "json")
        ];

        var engine = new RuleEngine(
        [
            new StaticRule("rule-b", "finding-b"),
            new StaticRule("rule-a", "finding-a")
        ]);

        var findings = engine.Evaluate(artifacts);

        Assert.Equal(4, findings.Count);
        Assert.Equal(
            [
                "a.json:finding-a",
                "a.json:finding-b",
                "b.json:finding-a",
                "b.json:finding-b"
            ],
            findings.Select(finding => $"{finding.Component}:{finding.Id}").ToArray());
    }

    [Fact]
    public void Evaluate_ReturnsFindingsInDeterministicOrder()
    {
        Artifact[] artifacts =
        [
            new Artifact("z.json", "generic", JsonNode.Parse("""{"value":"z"}"""), "json"),
            new Artifact("a.json", "generic", JsonNode.Parse("""{"value":"a"}"""), "json")
        ];

        var engine = new RuleEngine(
        [
            new StaticRule("rule-z", "finding-z"),
            new StaticRule("rule-a", "finding-a")
        ]);

        var findings = engine.Evaluate(artifacts);

        Assert.Equal(
            findings.OrderBy(finding => finding.Component, StringComparer.Ordinal)
                .ThenBy(finding => finding.Id, StringComparer.Ordinal)
                .ThenBy(finding => finding.Description, StringComparer.Ordinal)
                .Select(finding => finding.Id),
            findings.Select(finding => finding.Id));
    }

    [Fact]
    public void Evaluate_ReturnsEmptyCollectionWhenNothingMatches()
    {
        Artifact[] artifacts =
        [
            new Artifact("artifact.json", "generic", JsonNode.Parse("""{"value":"safe"}"""), "json")
        ];

        var engine = new RuleEngine([new EmptyRule()]);

        var findings = engine.Evaluate(artifacts);

        Assert.Empty(findings);
    }

    private sealed class StaticRule(string ruleId, string findingId) : IRule
    {
        public string Id => ruleId;

        public string Name => ruleId;

        public IEnumerable<Finding> Evaluate(Artifact artifact)
        {
            yield return new Finding(
                Id: findingId,
                Type: FindingTaxonomy.PromptInjection,
                Severity: SeverityLevels.Low,
                Component: artifact.Path,
                Description: $"{artifact.Path}:{findingId}",
                Evidence: new Evidence("summary"));
        }
    }

    private sealed class EmptyRule : IRule
    {
        public string Id => "empty-rule";

        public string Name => "Empty Rule";

        public IEnumerable<Finding> Evaluate(Artifact artifact)
            => [];
    }
}
