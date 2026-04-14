using System.Text.Json.Nodes;
using Detektor.Artifacts;
using Detektor.Findings;

namespace Detektor.Rules;

public sealed class ToolPermissionValidationRule : IRule
{
    private static readonly string[] ShellToolNames =
    [
        "bash",
        "cmd",
        "powershell",
        "sh",
        "shell",
        "terminal"
    ];

    public string Id => "tool-permission-validation-rule";

    public string Name => "Tool Permission Validation";

    public IEnumerable<Finding> Evaluate(Artifact artifact)
    {
        ArgumentNullException.ThrowIfNull(artifact);

        if (artifact.Content is not JsonObject rootObject)
        {
            yield break;
        }

        if (rootObject["tools"] is not JsonObject toolsObject)
        {
            yield break;
        }

        foreach (var tool in toolsObject.OrderBy(property => property.Key, StringComparer.Ordinal))
        {
            if (!ShellToolNames.Contains(tool.Key, StringComparer.OrdinalIgnoreCase) || tool.Value is not JsonObject toolConfig)
            {
                continue;
            }

            var enabled = toolConfig["enabled"]?.GetValue<bool?>() == true;
            var allowlistNode = toolConfig["allowlist"];
            var allowlist = allowlistNode as JsonArray;
            var hasAllowlist = allowlistNode is not null;
            var hasEmptyAllowlist = allowlist is not null && allowlist.Count == 0;

            if (hasEmptyAllowlist && enabled)
            {
                yield return CreateFinding(
                    artifact,
                    tool.Key,
                    FindingSeverity.Critical,
                    "empty-allowlist",
                    $"Tool '{tool.Key}' is enabled with an empty allowlist.",
                    "$.tools." + tool.Key + ".allowlist",
                    ArtifactContentNavigator.CreateSnippet(
                        ArtifactContentNavigator.ToNormalizedText(toolConfig),
                        "\"allowlist\":[]"));

                continue;
            }

            if (hasEmptyAllowlist)
            {
                yield return CreateFinding(
                    artifact,
                    tool.Key,
                    FindingSeverity.High,
                    "empty-allowlist",
                    $"Tool '{tool.Key}' declares an empty allowlist.",
                    "$.tools." + tool.Key + ".allowlist",
                    ArtifactContentNavigator.CreateSnippet(
                        ArtifactContentNavigator.ToNormalizedText(toolConfig),
                        "\"allowlist\":[]"));

                continue;
            }

            if (enabled && !hasAllowlist)
            {
                yield return CreateFinding(
                    artifact,
                    tool.Key,
                    FindingSeverity.High,
                    "enabled-without-allowlist",
                    $"Tool '{tool.Key}' is enabled without an allowlist restriction.",
                    "$.tools." + tool.Key + ".enabled",
                    ArtifactContentNavigator.CreateSnippet(
                        ArtifactContentNavigator.ToNormalizedText(toolConfig),
                        "\"enabled\":true"));
            }
        }
    }

    private Finding CreateFinding(
        Artifact artifact,
        string toolName,
        string severity,
        string findingSuffix,
        string description,
        string location,
        string snippet)
    {
        return new Finding(
            Id: $"{Id}:{artifact.Path}:{toolName}:{findingSuffix}",
            Type: FindingTaxonomy.ToolAbusePrivilegeEscalation,
            Severity: severity,
            Component: artifact.Path,
            Description: description,
            Evidence: new Evidence(
                Summary: description,
                Location: $"{artifact.Path}:{location}",
                Snippet: string.IsNullOrWhiteSpace(snippet) ? null : snippet));
    }
}
