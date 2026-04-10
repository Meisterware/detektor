namespace Detektor.Findings;

public sealed record Evidence(
    string Summary,
    string? Location = null,
    string? Snippet = null);
