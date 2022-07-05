using OtusHomeWork13.Infrastructure.Serialization;
using System.Diagnostics;
using System.Text.Json;

namespace OtusHomeWork13.Infrastructure.Services
{
    public class ProgramService : IProgramService
    {
        private readonly ISerializeCustom csvSerializator = new ObjectToCSV(separator: ";");
        private readonly ISerializeCustom jsonSerializator = new ObjectToJson(new JsonSerializerOptions { IncludeFields = true });

        public void Start()
        {
            var targetObject = new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
            var stopwatch = new Stopwatch();
            var countIteration = 10000;
            Console.WriteLine($"Исходный объект: {targetObject}");

            #region запуск custom сериализации в цикле
            Console.WriteLine($"Запуск custom сериализации...");
            stopwatch.Start();
            RepeatSerialize(csvSerializator, targetObject, countIteration);
            stopwatch.Stop();
            var stopwatchCustom = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Время на custom сериализацию: {countIteration} раз - {stopwatchCustom} мсек.");
            #endregion

            stopwatch.Reset();

            #region запуск JsonSerializer сериализации в цикле
            Console.WriteLine($"Запуск JsonSerializer сериализации...");
            stopwatch.Start();
            RepeatSerialize(jsonSerializator, targetObject, countIteration);
            stopwatch.Stop();
            var stopwatchJSON = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Время на JsonSerialize сериализацию: {countIteration} раз - {stopwatchJSON} мсек.");
            #endregion

            Console.WriteLine($"Разница во времени на сериализацию: {stopwatchCustom - stopwatchJSON} мсек.");

            #region сериализация объекта в файл
            WorkFromFile.Write(path: "f.csv", content: SerializeService.Serialize(csvSerializator, targetObject));
            WorkFromFile.Write(path: "f.json", content: SerializeService.Serialize(jsonSerializator, targetObject));
            #endregion

            stopwatch.Reset();
            
            #region чтение с файла и десериализация объекта custom
            var stringCSV = WorkFromFile.Read(path: "f.csv");
            stopwatch.Start();
            var objCSV = SerializeService.Deserialize<F>(csvSerializator, stringCSV);
            stopwatch.Stop();
            stopwatchCustom = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Десериализованный объект custom serialization: {objCSV}");
            Console.WriteLine($"Время на custom десериализацию: {stopwatchCustom} мсек.");
            #endregion

            stopwatch.Reset();

            #region чтение с файла и десериализация объекта JsonSerializer
            var stringJson = WorkFromFile.Read(path: "f.json");
            stopwatch.Start();
            var objJson = SerializeService.Deserialize<F>(jsonSerializator, stringJson);
            stopwatch.Stop();
            stopwatchJSON = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Десериализованный объект JsonSerialize: {objJson}");
            Console.WriteLine($"Время на JsonSerialize десериализацию: {stopwatchJSON} мсек.");
            #endregion

            Console.WriteLine($"Разница во времени на десериализацию: {stopwatchCustom - stopwatchJSON} мсек.");
        }

        private static void RepeatSerialize(ISerializeCustom serializator, object targetObject, int countIteration)
        {
            for (int i = 0; i < countIteration; i++)
            {
                SerializeService.Serialize(serializator, targetObject);
            }
        }

    }
}
