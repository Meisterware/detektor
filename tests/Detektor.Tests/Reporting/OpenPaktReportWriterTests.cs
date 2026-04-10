using System.Text.Json;
using Detektor.Findings;
using Detektor.Reporting.OpenPAKT;

namespace Detektor.Tests.Reporting;

public sealed class OpenPaktReportWriterTests
{
    [Fact]
    public void Write_SortsFindingsByComponentThenIdThenDescription_BeforeSerializing()
    {
        var writer = new OpenPaktReportWriter();
        IReadOnlyCollection<Finding> findings =
        [
            new("finding-b", "prompt_injection", "high", "b.json", "description-b", new Evidence("summary-b")),
            new("finding-c", "prompt_injection", "high", "a.json", "description-c", new Evidence("summary-c")),
            new("finding-a", "prompt_injection", "high", "a.json", "description-a", new Evidence("summary-a"))
        ];

        var json = writer.Write(new HashSet<Finding>(findings));

        using var document = JsonDocument.Parse(json);
        var serializedFindings = document.RootElement.GetProperty("Findings").EnumerateArray().ToArray();

        Assert.Collection(
            serializedFindings,
            finding =>
            {
                Assert.Equal("finding-a", finding.GetProperty("Id").GetString());
                Assert.Equal("a.json", finding.GetProperty("Component").GetString());
                Assert.Equal("description-a", finding.GetProperty("Description").GetString());
            },
            finding =>
            {
                Assert.Equal("finding-c", finding.GetProperty("Id").GetString());
                Assert.Equal("a.json", finding.GetProperty("Component").GetString());
                Assert.Equal("description-c", finding.GetProperty("Description").GetString());
            },
            finding =>
            {
                Assert.Equal("finding-b", finding.GetProperty("Id").GetString());
                Assert.Equal("b.json", finding.GetProperty("Component").GetString());
                Assert.Equal("description-b", finding.GetProperty("Description").GetString());
            });
    }
}
