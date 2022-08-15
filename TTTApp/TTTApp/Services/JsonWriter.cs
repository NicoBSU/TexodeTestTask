using Newtonsoft.Json;
using System.IO;
using TTTApp.Models;

namespace TTTApp.Services
{
    public class JsonWriter
    {
        public void WriteData(UserData userData, string path)
        {
            using (StreamWriter writer = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, userData);
            }
        }
    }
}
