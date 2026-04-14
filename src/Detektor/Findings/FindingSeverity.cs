using Detektor.OpenPAKT.Constants;

namespace Detektor.Findings;

public static class FindingSeverity
{
    public const string Critical = SeverityLevels.Critical;
    public const string High = SeverityLevels.High;
    public const string Medium = SeverityLevels.Medium;
    public const string Low = SeverityLevels.Low;
    public const string Informational = SeverityLevels.Informational;

    public static IReadOnlyCollection<string> All { get; } =
    [
        Critical,
        High,
        Medium,
        Low,
        Informational
    ];
}
