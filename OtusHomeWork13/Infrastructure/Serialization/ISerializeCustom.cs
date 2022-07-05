namespace OtusHomeWork13.Infrastructure.Serialization
{
    public interface ISerializeCustom
    {
        public string Serialize<T>(T targetObject);
        public T Deserialize<T>(string targetString);
    }
}
