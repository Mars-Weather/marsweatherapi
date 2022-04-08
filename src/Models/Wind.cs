using System.Text.Json.Serialization;

namespace MarsWeatherApi.Models
{
    public class Wind
    {
        public int Id { get; set; }
        public float? Average { get; set; }
        public float? Minimum { get; set; }
        public float? Maximum { get; set; }
        public string? MostCommonDirection { get; set; }

        [JsonIgnore] // JSON-annotaatioissa pitää olla Microsoftin kirjasto, ei Newtonsoftin
        public Sol? Sol { get; set; }
        public int SolId { get; set; }
         
    }
}