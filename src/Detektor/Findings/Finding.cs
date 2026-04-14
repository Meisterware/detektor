namespace Detektor.Findings;

public sealed record Finding
{
    public Finding(
        string Id,
        string Type,
        string Severity,
        string Component,
        string Description,
        Evidence Evidence)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(Id);
        ArgumentException.ThrowIfNullOrWhiteSpace(Type);
        ArgumentException.ThrowIfNullOrWhiteSpace(Severity);
        ArgumentException.ThrowIfNullOrWhiteSpace(Component);
        ArgumentException.ThrowIfNullOrWhiteSpace(Description);
        ArgumentNullException.ThrowIfNull(Evidence);

        if (!FindingTaxonomy.All.Contains(Type, StringComparer.Ordinal))
        {
            throw new ArgumentException($"Finding type '{Type}' is not a canonical taxonomy identifier.", nameof(Type));
        }

        if (!FindingSeverity.All.Contains(Severity, StringComparer.Ordinal))
        {
            throw new ArgumentException($"Finding severity '{Severity}' is not a canonical severity value.", nameof(Severity));
        }

        this.Id = Id;
        this.Type = Type;
        this.Severity = Severity;
        this.Component = Component;
        this.Description = Description;
        this.Evidence = Evidence;
    }

    public string Id { get; init; }

    public string Type { get; init; }

    public string Severity { get; init; }

    public string Component { get; init; }

    public string Description { get; init; }

    public Evidence Evidence { get; init; }
}
