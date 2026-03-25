using System.Text.Json;
using Detektor.Findings;
using Detektor.Reporting.OpenPAKT;

namespace Detektor.Tests.Reporting;

public sealed class OpenPaktReportWriterTests
{
    [Fact]
    public void Write_SortsFindingsByRuleIdThenMessage_BeforeSerializing()
    {
        var writer = new OpenPaktReportWriter();
        IReadOnlyCollection<Finding> findings =
        [
            new("rule-b", "message-b"),
            new("rule-a", "message-c"),
            new("rule-a", "message-a")
        ];

        var json = writer.Write(new HashSet<Finding>(findings));

        using var document = JsonDocument.Parse(json);
        var serializedFindings = document.RootElement.GetProperty("Findings").EnumerateArray().ToArray();

        Assert.Collection(
            serializedFindings,
            finding =>
            {
                Assert.Equal("rule-a", finding.GetProperty("RuleId").GetString());
                Assert.Equal("message-a", finding.GetProperty("Message").GetString());
            },
            finding =>
            {
                Assert.Equal("rule-a", finding.GetProperty("RuleId").GetString());
                Assert.Equal("message-c", finding.GetProperty("Message").GetString());
            },
            finding =>
            {
                Assert.Equal("rule-b", finding.GetProperty("RuleId").GetString());
                Assert.Equal("message-b", finding.GetProperty("Message").GetString());
            });
    }
}
