using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TTTApp.Enums;

namespace TTTApp.Models
{
    public class User
    {
        [JsonProperty(PropertyName = "User")]
        public string UserName { get; set; }
        public int Rank { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TrainingStatus Status { get; set; }
        public int Steps { get; set; }
    }
}
