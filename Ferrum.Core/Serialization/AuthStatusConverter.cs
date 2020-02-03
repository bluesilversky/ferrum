using Ferrum.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ferrum.Core.Serialization
{
    public class AuthStatusConverter : JsonConverter<AuthStatus>
    {
        public override AuthStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var strValue = reader.GetString();
            var success = Enum.TryParse<AuthStatus>(strValue, out var result);
            return success ? result : AuthStatus.Unknown;
        }

        public override void Write(Utf8JsonWriter writer, AuthStatus value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
