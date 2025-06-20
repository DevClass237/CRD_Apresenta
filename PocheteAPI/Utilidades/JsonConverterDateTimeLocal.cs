﻿using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PocheteAPI.Utilidades {
    public class JsonConverterDateTimeLocal : JsonConverter<DateTime> {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            return DateTime.Parse(reader.GetString() ?? "").ToLocalTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:ss.fff"));
        }
    }
}
