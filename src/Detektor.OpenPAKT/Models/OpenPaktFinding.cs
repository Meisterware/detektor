namespace Detektor.OpenPAKT.Models;

public sealed class OpenPaktFinding
{
    public string RuleId { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;

    public string Severity { get; init; } = Constants.SeverityLevels.Info;
}
