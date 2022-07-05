using OtusHomeWork13.Infrastructure.Serialization;

namespace OtusHomeWork13.Infrastructure.Services
{
    public static class SerializeService
    {
        public static string Serialize<T>(ISerializeCustom serializator, T targetObject) => serializator.Serialize(targetObject);

        public static T Deserialize<T>(ISerializeCustom serializator, string targetString) => serializator.Deserialize<T>(targetString);
    }
}
