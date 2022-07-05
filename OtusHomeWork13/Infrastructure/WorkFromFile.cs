namespace OtusHomeWork13.Infrastructure
{
    public static class WorkFromFile
    {
        public static void Write(string path, string content)
        {
            using var sw = new StreamWriter(path);
            sw.Write(content);
            sw.Close();
        }

        public static string Read(string path)
        {
            using var src = new StreamReader(path);
            return src.ReadToEnd();
        }

    }
}
