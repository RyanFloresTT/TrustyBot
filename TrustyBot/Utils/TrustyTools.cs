using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustyBot.Utils
{
    public static class TrustyTools
    {
        public static List<T> ReadFromJSON<T>(this string filePath)
        {
            using StreamReader reader = new(filePath);
            var json = reader.ReadToEnd();
            List<T> data = JsonConvert.DeserializeObject<List<T>>(json);
            return data;
        }
    }
}
