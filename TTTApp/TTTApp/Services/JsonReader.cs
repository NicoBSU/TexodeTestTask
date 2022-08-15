using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TTTApp.Consts;
using TTTApp.Models;

namespace TTTApp.Services
{
    public class JsonReader
    {
        public List<User> LoadData(int jsonFileNumber)
        {
            var path = PathConsts.JsonFilesPath + $"day{jsonFileNumber}.json";

            var fileExists = File.Exists(path);
            if (!fileExists)
            {
                File.CreateText(path).Dispose();
                return new List<User>();
            }
            using (var reader = File.OpenText(path))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<User>>(fileText);
            }
            
        }
    }
}
