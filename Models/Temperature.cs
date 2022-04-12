using System.Text.Json.Serialization;

namespace MarsWeatherApi.Models
{
    public class Temperature
    {
        public int Id { get; set; }
        public float? Average { get; set; } // Fahrenheit
        public float? Minimum { get; set; } // Fahrenheit
        public float? Maximum { get; set; } // Fahrenheit
        [JsonIgnore]
        public Sol? Sol { get; set; }
        public int SolId { get; set; }
    }
}