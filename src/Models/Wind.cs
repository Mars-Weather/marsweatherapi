using System.Text.Json.Serialization;

namespace MarsWeatherApi.Models
{
    public class Wind
    {
        public int Id { get; set; }
        public float? Average { get; set; } // Metres per second, m/s
        public float? Minimum { get; set; } // Metres per second, m/s
        public float? Maximum { get; set; } // Metres per second, m/s
        public string? MostCommonDirection { get; set; } // 16-wind compass rose, e.g. N for north, NNE for north-northeast
        [JsonIgnore] // JSON-annotaatioissa pitää olla Microsoftin kirjasto, ei Newtonsoftin
        public Sol? Sol { get; set; }
        public int SolId { get; set; }         
    }
}