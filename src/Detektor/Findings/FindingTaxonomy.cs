using Detektor.OpenPAKT.Constants;

namespace Detektor.Findings;

public static class FindingTaxonomy
{
    public const string PromptInjection = FindingTypes.PromptInjection;
    public const string ToolAbusePrivilegeEscalation = FindingTypes.ToolAbusePrivilegeEscalation;
    public const string DataExfiltration = FindingTypes.DataExfiltration;
    public const string SensitiveDataExposure = FindingTypes.SensitiveDataExposure;
    public const string MemoryPoisoning = FindingTypes.MemoryPoisoning;
    public const string GoalHijacking = FindingTypes.GoalHijacking;
    public const string ExcessiveAutonomy = FindingTypes.ExcessiveAutonomy;
    public const string CascadingFailures = FindingTypes.CascadingFailures;
    public const string DenialOfWallet = FindingTypes.DenialOfWallet;

    public static IReadOnlyCollection<string> All { get; } =
    [
        PromptInjection,
        ToolAbusePrivilegeEscalation,
        DataExfiltration,
        SensitiveDataExposure,
        MemoryPoisoning,
        GoalHijacking,
        ExcessiveAutonomy,
        CascadingFailures,
        DenialOfWallet
    ];
}
