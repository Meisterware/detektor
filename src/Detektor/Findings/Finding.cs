namespace Detektor.Findings;

public sealed record Finding(
    string Id,
    string Type,
    string Severity,
    string Component,
    string Description,
    Evidence Evidence);
