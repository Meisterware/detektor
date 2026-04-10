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
                .OrderBy(finding => finding.Component, StringComparer.Ordinal)
                .ThenBy(finding => finding.Id, StringComparer.Ordinal)
                .ThenBy(finding => finding.Description, StringComparer.Ordinal)
                .Select(finding => new OpenPaktFinding
                {
                    Id = finding.Id,
                    Type = finding.Type,
                    Severity = finding.Severity,
                    Component = finding.Component,
                    Description = finding.Description
                }));

        return OpenPaktJsonSerializer.Serialize(report);
    }
}
