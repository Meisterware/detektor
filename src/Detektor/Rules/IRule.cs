using Detektor.Artifacts;
using Detektor.Findings;

namespace Detektor.Rules;

public interface IRule
{
    string Id { get; }

    string Name { get; }

    IEnumerable<Finding> Evaluate(Artifact artifact);
}
