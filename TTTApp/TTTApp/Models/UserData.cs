using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TTTApp.Enums;

namespace TTTApp.Models
{
    public class UserData
    {
        public string UserName { get; set; }
        public List<int> Ranks { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public List<TrainingStatus> Statuses { get; set; }
        public List<int> DailyResultsList { get; set; }
        public double AverageSteps { get; set; }
        public int BestResult { get; set; }
        public int WorstResult { get; set; }

        public bool StepsComparisonPassed { get; set; } = false;
    }
}
