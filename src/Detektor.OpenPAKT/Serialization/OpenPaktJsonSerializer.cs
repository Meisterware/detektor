using System.Text.Json;

namespace Detektor.OpenPAKT.Serialization;

public static class OpenPaktJsonSerializer
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    public static string Serialize<T>(T value)
    {
        return JsonSerializer.Serialize(value, SerializerOptions);
    }
}
