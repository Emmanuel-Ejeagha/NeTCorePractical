using System;
using System.Text.Json;

namespace GetJokes;

public class Utf8JsonWrite
{
  public static void FindWithJsonDom(Stream inStream, Stream outStream, string category)
    {
        var writerOptions = new JsonWriterOptions { Indented = true };
        using var writer = new Utf8JsonWriter(outStream, writerOptions);
        writer.WriteStartArray();

        using var jsonDoc = JsonDocument.Parse(inStream);
        foreach (var jokeElement in jsonDoc.RootElement.EnumerateArray())
        {
            string? type = jokeElement
              .GetProperty("type")
              .GetString();
            if (string.Equals(category, type, StringComparison.OrdinalIgnoreCase))
            {
                jokeElement.WriteTo(writer);
            }
        }
        writer.WriteEndArray();
    }
}
