using Newtonsoft.Json;
using TrustyBot.Modules.GameRouletteCommand;

namespace TrustyBot.Utils
{
    public static class GameDataFileUtils
    {
        private static readonly JsonSerializerSettings options
            = new() { NullValueHandling = NullValueHandling.Ignore };

        public static void Write(object obj, string filePath)
        {
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, options);
            File.WriteAllText(filePath, jsonString);
        }

        public static void Add(GameData data, string fileName)
        {
            var contents = fileName.ReadFromJSON<GameData>();
            contents.Add(data);
            Write(contents, fileName);
        }
        public static void Remove(string gameName, string fileName)
        {
            var contents = fileName.ReadFromJSON<GameData>();
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

    }
}
