using Ferrum.Core.Extensions;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ferrum.Core.Utils
{
    public static class Serialization
    {
        private static readonly JsonSerializerOptions _defaultOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        public static JsonSerializerOptions DefaultOptions()
        {
            _defaultOptions.Converters.AddEnumSerializers();
            return _defaultOptions;
        }

        public static async Task<T> DeserializeAsync<T>(this HttpContent jsonHttpContent) where T : class, new()
        {
            var stringContent = await jsonHttpContent.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(stringContent, DefaultOptions());
        }
    }
}
