namespace Detektor.OpenPAKT.Models;

public sealed class OpenPaktReport
{
    public string SchemaVersion { get; init; } = "0.1-placeholder";

    public List<OpenPaktFinding> Findings { get; } = [];
}
