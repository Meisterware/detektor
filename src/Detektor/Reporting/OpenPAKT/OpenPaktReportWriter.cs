using Detektor.Findings;
using Detektor.OpenPAKT.Models;
using Detektor.OpenPAKT.Serialization;

namespace Detektor.Reporting.OpenPAKT;

public sealed class OpenPaktReportWriter
{
    public string Write(IReadOnlyCollection<Finding> findings)
    {
        var report = new OpenPaktReport();

        report.Findings.AddRange(
            findings
                .OrderBy(finding => finding.RuleId, StringComparer.Ordinal)
                .ThenBy(finding => finding.Message, StringComparer.Ordinal)
                .Select(finding => new OpenPaktFinding
                {
                    RuleId = finding.RuleId,
                    Message = finding.Message
                }));

        return OpenPaktJsonSerializer.Serialize(report);
    }
}
