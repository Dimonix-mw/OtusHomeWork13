using System.Text.Json;

namespace OtusHomeWork13.Infrastructure.Serialization
{
    public class ObjectToJson : ISerializeCustom
    {
        private readonly JsonSerializerOptions _options;
        public ObjectToJson(JsonSerializerOptions options)
        {
            _options = options;
        }

        public T Deserialize<T>(string targetString)
        {
            return JsonSerializer.Deserialize<T>(targetString, _options);
        }

        public string Serialize<T>(T targetObject)
        {
            return JsonSerializer.Serialize(targetObject, _options);
        }
    }
}
