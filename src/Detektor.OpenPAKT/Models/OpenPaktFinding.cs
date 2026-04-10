namespace Detektor.OpenPAKT.Models;

public sealed class OpenPaktFinding
{
    public string Id { get; init; } = string.Empty;

    public string Type { get; init; } = string.Empty;

    public string Severity { get; init; } = Constants.SeverityLevels.Informational;

    public string Component { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;
}
