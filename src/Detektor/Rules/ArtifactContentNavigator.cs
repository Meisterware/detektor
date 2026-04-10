using System.Text.Json;
using System.Text.Json.Nodes;

namespace Detektor.Rules;

internal static class ArtifactContentNavigator
{
    public static IEnumerable<(string Path, string Value)> EnumerateStrings(JsonNode? node)
    {
        foreach (var entry in EnumerateStrings(node, "$"))
        {
            yield return entry;
        }
    }

    public static string ToNormalizedText(JsonNode? node)
        => node?.ToJsonString(new JsonSerializerOptions { WriteIndented = false }) ?? string.Empty;

    public static string CreateSnippet(string content, string match)
    {
        if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(match))
        {
            return string.Empty;
        }

        var index = content.IndexOf(match, StringComparison.OrdinalIgnoreCase);

        if (index < 0)
        {
            return string.Empty;
        }

        const int radius = 40;
        var start = Math.Max(0, index - radius);
        var length = Math.Min(content.Length - start, match.Length + (radius * 2));

        return content.Substring(start, length);
    }

    private static IEnumerable<(string Path, string Value)> EnumerateStrings(JsonNode? node, string path)
    {
        switch (node)
        {
            case null:
                yield break;

            case JsonValue jsonValue when jsonValue.TryGetValue<string>(out var stringValue):
                yield return (path, stringValue);
                yield break;

            case JsonObject jsonObject:
                foreach (var property in jsonObject.OrderBy(property => property.Key, StringComparer.Ordinal))
                {
                    var childPath = $"{path}.{property.Key}";

                    foreach (var child in EnumerateStrings(property.Value, childPath))
                    {
                        yield return child;
                    }
                }

                yield break;

            case JsonArray jsonArray:
                for (var index = 0; index < jsonArray.Count; index++)
                {
                    var childPath = $"{path}[{index}]";

                    foreach (var child in EnumerateStrings(jsonArray[index], childPath))
                    {
                        yield return child;
                    }
                }

                yield break;
        }
    }
}
