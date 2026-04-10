using Detektor.Artifacts;
using Detektor.Findings;

namespace Detektor.Rules;

public sealed class RuleEngine
{
    private readonly IReadOnlyList<IRule> _rules;

    public RuleEngine(IEnumerable<IRule> rules)
    {
        ArgumentNullException.ThrowIfNull(rules);

        _rules = rules
            .OrderBy(rule => rule.Id, StringComparer.Ordinal)
            .ToArray();
    }

    public IReadOnlyList<Finding> Evaluate(IReadOnlyList<Artifact> artifacts)
    {
        ArgumentNullException.ThrowIfNull(artifacts);

        var findings = new List<Finding>();

        foreach (var artifact in artifacts.OrderBy(artifact => artifact.Path, StringComparer.Ordinal))
        {
            foreach (var rule in _rules)
            {
                findings.AddRange(rule.Evaluate(artifact));
            }
        }

        return findings
            .OrderBy(finding => finding.Component, StringComparer.Ordinal)
            .ThenBy(finding => finding.Id, StringComparer.Ordinal)
            .ThenBy(finding => finding.Description, StringComparer.Ordinal)
            .ToArray();
    }
}
