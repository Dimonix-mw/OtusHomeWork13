using System.Reflection;
using System.Text;

namespace OtusHomeWork13.Infrastructure.Serialization
{
    public class ObjectToCSV : ISerializeCustom
    {
        private string _separator;
        public ObjectToCSV(string separator)
        {
            _separator = separator;
        }

        public T Deserialize<T>(string targetString)
        {
            var lines = targetString.Split('\r');
            if (lines.Length == 0)
            {
                return Activator.CreateInstance<T>();
            }
            var header = lines[0].Split(_separator);
            var data = lines[1].Split(_separator);
            var instance = Activator.CreateInstance<T>();

            FieldInfo[] fields = typeof(T).GetFields();
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            for (int i = 0; i < header.Length; i++)
            {
                var field = fields.FirstOrDefault(x => x.Name == header[i]);
                if (field != null)
                {
                    Type fieldType = field.FieldType;
                    field.SetValue(instance, Convert.ChangeType(data[i], fieldType));
                }

                var prop = propertyInfos.FirstOrDefault(x => x.Name == header[i]);
                if (prop != null)
                {
                    prop.SetValue(instance, data[i]);
                }
            }
            return instance;
        }

        public string Serialize<T>(T targetObject)
        {
            FieldInfo[] fields = typeof(T).GetFields();
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();

            var csvString = new StringBuilder();
            var header = string.Join(_separator, fields.Select(f => f.Name).Concat(propertyInfos.Select(p => p.Name)));
            csvString.AppendLine(header);
            var data = string.Join(_separator, fields.Select(f => f.GetValue(targetObject) ?? "")
                .Concat(propertyInfos.Select(p => p.GetValue(targetObject) ?? "")));
            csvString.AppendLine(data);
            return csvString.ToString();
        }
    }
}
