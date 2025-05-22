using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarlssonsWPF.Helpers
{
    public class DateOnlyConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            if (DateTime.TryParse(value, out var result))
                return result;
            return null;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
                writer.WriteStringValue(value.Value.ToString("yyyy-MM-dd")); // UDEN klokkeslæt
            else
                writer.WriteNullValue();
        }
    }
}
