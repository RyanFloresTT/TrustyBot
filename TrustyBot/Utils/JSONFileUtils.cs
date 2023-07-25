using Discord;
using Newtonsoft.Json;
using TrustyBot.Modules.GameRouletteCommand;

namespace TrustyBot.Utils
{
    public static class JSONFileUtils
    {
        private static readonly JsonSerializerSettings options
            = new() { NullValueHandling = NullValueHandling.Ignore };

        public static void Write(object obj, string filePath)
        {
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, options);
            File.WriteAllText(filePath, jsonString);
        }

        public static void AddToJSON(this JSONData data, string fileName)
        {
            var contents = fileName.ReadFromJSON<JSONData>();
            contents.Add(data);
            Write(contents, fileName);
        }
        public static void RemoveFromJSON(this string gameName, string fileName)
        {
            var contents = fileName.ReadFromJSON<JSONData>();
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i].Name == gameName)
                {
                    contents.Remove(contents[i]);
                    break;
                }
            }
            Write(contents, fileName);
        }
        public static List<JSONData> ReadFromJSON<JSONData>(this string filePath)
        {
            using StreamReader reader = new(filePath);
            var json = reader.ReadToEnd();
            List<JSONData> data = JsonConvert.DeserializeObject<List<JSONData>>(json);
            return data;
        }

        public static T Rand<T>(this List<T> list)
        {
            var random = new Random();
            return list[random.Next(0, list.Count)];
        }
    }
}
