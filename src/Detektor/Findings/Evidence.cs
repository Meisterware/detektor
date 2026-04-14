namespace Detektor.Findings;

public sealed record Evidence
{
    public Evidence(string Summary, string? Location = null, string? Snippet = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Summary);

        this.Summary = Summary;
        this.Location = Location;
        this.Snippet = Snippet;
    }

    public string Summary { get; init; }

    public string? Location { get; init; }

    public string? Snippet { get; init; }
}
